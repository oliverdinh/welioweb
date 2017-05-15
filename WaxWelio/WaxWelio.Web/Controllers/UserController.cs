using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

using Newtonsoft.Json;
using WaxWelio.Abstractions;
using WaxWelio.Common;
using WaxWelio.Common.Config;
using WaxWelio.Common.Enum;
using WaxWelio.Common.Exception;
using WaxWelio.Common.Object;
using WaxWelio.Entities.Models;
using WaxWelio.Entities.ViewModels;
using WaxWelio.Entities;
using WaxWelio.Entities.Result;

namespace WaxWelio.Web.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class UserController : BaseController
    {
        // GET: User
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //[AuthorCustom(new UserRole[] { UserRole.RoleHospitalAdmin, UserRole.RoleAdmin })]
        public ActionResult Index()
        {
            ViewBag.idSelected = "users";
            return View();
        }

        //[AuthorCustom(new UserRole[] { UserRole.RoleHospitalAdmin, UserRole.RoleAdmin })]
        public ActionResult GetListUser(JQueryDataTableParamModel param)
        {
            if (Session["auth_info"] != null)
            {
                try
                {
                    var authInfo = (AuthInfo)Session["auth_info"];
                    var listUser = _userService.ListUsers(authInfo.CurrentSelectedClinic.ClinicId, param.iDisplayStart, param.iDisplayLength, string.IsNullOrEmpty(param.sSortDir_0) ? "ASC" : param.sSortDir_0.ToUpper(), "");
                    ViewBag.userId = authInfo.DoctorId;
                    ViewBag.doctorId = authInfo.CurrentSelectedClinic.ClinicId;
                    var index = 1;
                    if (param.iDisplayStart > 0)
                    {
                        index += param.iDisplayStart;
                    }
                    foreach (var user in listUser)
                    {
                        if (!string.IsNullOrEmpty(user.TitleUser))
                        {
                            user.FirstName = ((Title)Enum.Parse(typeof(Title), user.TitleUser)).DescriptionAttr() + " " + user.FirstName;
                        }
                        if (user.DoctorId.Equals(authInfo.DoctorId))
                        {
                            user.LastName += " (Me)";
                        }
                        user.Status = ((Status)user.Actived).DescriptionAttr();
                        user.ShortDoctorInfo.StrUserType = ((UserType)user.ShortDoctorInfo.UserType).DescriptionAttr();
                        user.ShortDoctorInfo.StrAccessType = ((UserRole)user.ShortDoctorInfo.AccessType).DescriptionAttr();
                        user.ShortDoctorInfo.StrClinicUserActived = ((Status)user.ShortDoctorInfo.ClinicUserActived).DescriptionAttr();
                        index++;
                    }
                    return Json(new
                    {
                        draw = param.sEcho,
                        recordsTotal = listUser.Count,
                        recordsFiltered = _userService.Total(),
                        data = listUser
                    }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    return Json(new
                    {
                        draw = param.sEcho,
                        recordsTotal = 0,
                        recordsFiltered = 0,
                        data = new List<UserModel>()
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                TempData[GlobalConstant.ErrorTemp] = GlobalConstant.SessionExpiredOrNotLogin;
                return RedirectToAction("Index", "Home");
            }
        }

        //[AuthorCustom(new UserRole[] { UserRole.RoleHospitalAdmin, UserRole.RoleAdmin })]
        public ActionResult Edit(string id)
        {
            ViewBag.idSelected = "users";
            if (Session["auth_info"] != null)
            {
                try
                {
                    var authInfo = (AuthInfo)Session["auth_info"];
                    if (id.Equals(authInfo.DoctorId))
                    {
                        ViewBag.isOtherAccount = false;
                    }
                    else
                    {
                        ViewBag.isOtherAccount = true;
                    }
                    UserModel user = _userService.GetUserDetails(id);
                    int userRole = Array.Find(user.ClinicUsers, a => a.Clinic.ClinicId == authInfo.CurrentSelectedClinic.ClinicId).AccessType;
                    int userType = Array.Find(user.ClinicUsers, a => a.Clinic.ClinicId == authInfo.CurrentSelectedClinic.ClinicId).UserType;
                    string clinicName = Array.Find(user.ClinicUsers, a => a.Clinic.ClinicId == authInfo.CurrentSelectedClinic.ClinicId).Clinic.ClinicName;
                    string clinicUserId = Array.Find(user.ClinicUsers, a => a.Clinic.ClinicId == authInfo.CurrentSelectedClinic.ClinicId).ClinicUserId;
                    ViewBag.listUserRole =
                        new SelectList(Enum.GetValues(typeof(UserRole)).Cast<UserRole>().Select(v => new SelectListItem
                        {
                            Text = v.DisplayName(),
                            Value = ((int)v).ToString(),
                        }).Where(v => Int32.Parse(v.Value) != 0), "Value", "Text", userRole);

                    ViewBag.listUserType =
                        new SelectList(Enum.GetValues(typeof(UserType)).Cast<UserType>().Select(v => new SelectListItem
                        {
                            Text = v.DisplayName(),
                            Value = ((int)v).ToString(),
                        }).Where(v => Int32.Parse(v.Value) != 0), "Value", "Text", userType);

                    ViewBag.listUserTitle =
                        new SelectList(Enum.GetValues(typeof(Title)).Cast<Title>().Select(v => new SelectListItem
                        {
                            Text = v.DisplayName(),
                            Value = v.ToString(),
                        }), "Value", "Text", user.TitleUser);

                    ViewBag.UserSubType = userType;
                    ViewBag.UserRole = userRole;
                    ViewBag.clinicName = clinicName;
                    ViewBag.lengthClinic = user.ClinicUsers.Length;
                    ViewBag.clinicUserId = clinicUserId;
                    user.AccessType = userRole;
                    user.UserType = userType;
                    return View(user);
                }
                catch (Exception ex)
                {
                    TempData[GlobalConstant.ErrorTemp] = ex.Message;
                    return View(new UserModel());
                }
            }
            else
            {
                TempData[GlobalConstant.ErrorTemp] = GlobalConstant.SessionExpiredOrNotLogin;
                return RedirectToAction("Index", "Home");
            }
        }

        //[AuthorCustom(new UserRole[] { UserRole.RoleHospitalAdmin, UserRole.RoleAdmin })]
        [HttpPost]
        public ActionResult Edit(UserModel model, string clinicUserId)
        {
            if (Session["auth_info"] != null)
            {
                try
                {
                    var authInfo = (AuthInfo)Session["auth_info"];
                    var dataurl = Request["image-data"];
                    dataurl = dataurl.Replace("data:image/png;base64,", "");
                    byte[] data = Convert.FromBase64String(dataurl);
                    var fileName = Request["image-name"];
                    if (data.Length > 0)
                    {
                        model.FileName = fileName;
                        model.Imagefile = data;
                    }
                    var dataModel = new UpdateClinicUserModel
                    {
                        ClinicUserId = clinicUserId,
                        DoctorId = model.DoctorId,
                        AccessType = model.AccessType,
                        UserType = model.UserType,
                        FileName = string.IsNullOrEmpty(fileName) ? null : ("/Upload/Patient_Images/" + fileName),
                        Imagefile = data,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Title = model.TitleUser
                    };
                    _userService.UpdateOtherUser(dataModel);
                    if (model.DoctorId.Equals(authInfo.DoctorId))
                    {
                        var tempAuthInfo = _userService.GetAuthInfo(authInfo.Office365);
                        tempAuthInfo.CurrentSelectedClinic = authInfo.CurrentSelectedClinic;
                        Session["auth_info"] = tempAuthInfo;
                    }
                    TempData[GlobalConstant.ErrorTemp] = "User details successfully updated.";
                    return RedirectToAction("Profile", new { id = model.DoctorId });
                }
                catch (ApiException ex)
                {
                    TempData[GlobalConstant.ErrorTemp] = ex.Message;
                    return View(model);
                }
            }
            else
            {
                TempData[GlobalConstant.ErrorTemp] = GlobalConstant.SessionExpiredOrNotLogin;
                return RedirectToAction("Index", "Home");
            }
        }

        //[AuthorCustom(new UserRole[] { UserRole.RoleHospitalAdmin, UserRole.RoleAdmin, UserRole.RoleDoctor })]
        public ActionResult Profile(string id)
        {
            ViewBag.idSelected = "users";
            if (Session["auth_info"] != null)
            {
                try
                {
                    var authInfo = (AuthInfo)Session["auth_info"];
                    //if (id.Equals(authInfo.DoctorId))
                    //{
                    //    return RedirectToAction("MyProfile");
                    //}
                    UserModel user = _userService.GetUserDetails(id);
                    ViewBag.clinicId = authInfo.CurrentSelectedClinic.ClinicId;
                    //int userType =
                    //    Array.Find(user.DoctorInfos, a => a.Hospital.Id == BaseApiHeader.HospitalSelected).SubType;
                    //ViewBag.userType = userType;
                    return View(user);
                }
                catch (ApiException ex)
                {
                    TempData[GlobalConstant.ErrorTemp] = ex.Message;
                    return RedirectToAction("Index", "User");
                }
            }
            else
            {
                TempData[GlobalConstant.ErrorTemp] = GlobalConstant.SessionExpiredOrNotLogin;
                return RedirectToAction("Index", "Home");
            }
        }

        //[AuthorCustom(new UserRole[] { UserRole.RoleHospitalAdmin, UserRole.RoleAdmin, UserRole.RoleDoctor })]
        public ActionResult MyProfile()
        {
            ViewBag.idSelected = "my-profile";
            if (Session["auth_info"] != null)
            {
                var authInfo = (AuthInfo)Session["auth_info"];
                try
                {
                    UserModel user = _userService.GetUserDetails(authInfo.DoctorId);

                    return View(user);
                }
                catch (ApiException ex)
                {
                    TempData[GlobalConstant.ErrorTemp] = ex.Message;
                    return View(new UserModel());
                }
            }
            else
            {
                TempData[GlobalConstant.ErrorTemp] = GlobalConstant.SessionExpiredOrNotLogin;
                return RedirectToAction("Index", "Home");
            }
        }

        //[AuthorCustom(new UserRole[] { UserRole.RoleHospitalAdmin, UserRole.RoleAdmin, UserRole.RoleDoctor })]
        public ActionResult EditProfile()
        {
            ViewBag.idSelected = "my-profile";
            if (Session["auth_info"] != null)
            {
                var authInfo = (AuthInfo)Session["auth_info"];
                try
                {
                    UserModel user = _userService.GetUserDetails(authInfo.DoctorId);
                    var hospital = user.ClinicUsers.FirstOrDefault(x => x.Clinic.ClinicId == authInfo.CurrentSelectedClinic.ClinicId);
                    int userRole = hospital.AccessType;
                    int userType = hospital.UserType;

                    ViewBag.listUserRole =
                        new SelectList(Enum.GetValues(typeof(UserRole)).Cast<UserRole>().Select(v => new SelectListItem
                        {
                            Text = v.DisplayName(),
                            Value = ((int)v).ToString(),
                        }).Where(v => Int32.Parse(v.Value) != 0), "Value", "Text", userRole);

                    ViewBag.listUserType =
                        new SelectList(Enum.GetValues(typeof(UserType)).Cast<UserType>().Select(v => new SelectListItem
                        {
                            Text = v.DisplayName(),
                            Value = ((int)v).ToString(),
                        }).Where(v => Int32.Parse(v.Value) != 0), "Value", "Text", userType);

                    ViewBag.listUserTitle =
                        Enum.GetValues(typeof(Title)).Cast<Title>().Select(v => new SelectListItem
                        {
                            Text = v.DisplayName(),
                            Value = v.ToString(),
                        });

                    ViewBag.UserSubType = userType;
                    ViewBag.UserRole = userRole;
                    ViewBag.countClinic = authInfo.DoctorClinics.Count();
                    ViewBag.ClinicUserId = hospital.ClinicUserId;
                    user.AccessType = hospital.AccessType;
                    user.UserType = hospital.UserType;
                    return View(user);
                }
                catch (ApiException ex)
                {
                    TempData[GlobalConstant.ErrorTemp] = ex.Message;
                    return View(new UserModel());
                }
            }
            else
            {
                TempData[GlobalConstant.ErrorTemp] = GlobalConstant.SessionExpiredOrNotLogin;
                return RedirectToAction("Index", "Home");
            }
        }

        //[AuthorCustom(new UserRole[] { UserRole.RoleHospitalAdmin, UserRole.RoleAdmin, UserRole.RoleDoctor })]
        [HttpPost]
        public ActionResult EditProfile(UserModel model, string clinicUserId)
        {
            if (Session["auth_info"] != null)
            {
                try
                {
                    var authInfo = (AuthInfo)Session["auth_info"];
                    var dataurl = Request["image-data"];
                    dataurl = dataurl.Replace("data:image/png;base64,", "");
                    byte[] data = Convert.FromBase64String(dataurl);
                    var fileName = Request["image-name"];
                    if (data.Length > 0)
                    {
                        model.FileName = fileName;
                        model.Imagefile = data;
                    }
                    var dataModel = new UpdateClinicUserModel
                    {
                        ClinicUserId = clinicUserId,
                        DoctorId = model.DoctorId,
                        AccessType = model.AccessType,
                        UserType = model.UserType,
                        FileName = string.IsNullOrEmpty(fileName) ? null : ("/Upload/Patient_Images/" + fileName),
                        Imagefile = data.Length > 0 ? data : null,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Title = model.TitleUser
                    };
                    _userService.UpdateOtherUser(dataModel);
                    var tempAuthInfo = _userService.GetAuthInfo(authInfo.Office365);
                    tempAuthInfo.CurrentSelectedClinic = authInfo.CurrentSelectedClinic;
                    Session["auth_info"] = tempAuthInfo;
                    return RedirectToAction("MyProfile");
                }
                catch (Exception ex)
                {
                    TempData[GlobalConstant.ErrorTemp] = ex.Message;
                    return RedirectToAction("EditProfile", "User");
                }
            }
            else
            {
                TempData[GlobalConstant.ErrorTemp] = GlobalConstant.SessionExpiredOrNotLogin;
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult CloseProfile()
        {
            ViewBag.idSelected = "users";
            return View();
        }

        //[AuthorCustom(new UserRole[] { UserRole.RoleHospitalAdmin, UserRole.RoleAdmin })]
        public ActionResult Add()
        {
            if (Session["auth_info"] != null)
            {
                ViewBag.listUserRole = Enum.GetValues(typeof(UserRole)).Cast<UserRole>().Select(v => new SelectListItem
                {
                    Text = v.DisplayName(),
                    Value = ((int)v).ToString()
                });

                ViewBag.listUserType = Enum.GetValues(typeof(UserType)).Cast<UserType>().Select(v => new SelectListItem
                {
                    Text = v.DisplayName(),
                    Value = ((int)v).ToString()
                });//.Where(v => Int32.Parse(v.Value) != 99 && Int32.Parse(v.Value) != 0);

                ViewBag.listUserTitle = Enum.GetValues(typeof(Title)).Cast<Title>().Select(v => new SelectListItem
                {
                    Text = v.DisplayName(),
                    Value = v.ToString()
                });
                UserModel model;
                if (TempData[GlobalConstant.UserModel] != null)
                {
                    model = (UserModel)TempData[GlobalConstant.UserModel];
                }
                else
                {
                    model = new UserModel();
                }
                ViewBag.idSelected = "users";
                return View(model);
            }
            else
            {
                TempData[GlobalConstant.ErrorTemp] = GlobalConstant.SessionExpiredOrNotLogin;
                return RedirectToAction("Index", "Home");
            }
        }

        //[AuthorCustom(new UserRole[] { UserRole.RoleHospitalAdmin, UserRole.RoleAdmin })]
        [HttpPost]
        public ActionResult Add(UserModel model)
        {
            if (Session["auth_info"] != null)
            {
                try
                {
                    var authInfo = (AuthInfo)Session["auth_info"];
                    var checkDoctorExist = _userService.CheckDoctorExists(authInfo.CurrentSelectedClinic.ClinicId, model.Email);
                    var dataurl = Request["image-data"];
                    dataurl = dataurl.Replace("data:image/png;base64,", "");
                    byte[] data = Convert.FromBase64String(dataurl);
                    var fileName = Request["image-name"];
                    if (data.Length > 0)
                    {
                        model.FileName = fileName;
                        model.Imagefile = data;
                    }
                    var dataModel = new CreateClinicUserModel
                    {
                        ClinicId = authInfo.CurrentSelectedClinic.ClinicId,
                        ClinicName = authInfo.CurrentSelectedClinic.ClinicName,
                        AccessType = model.AccessType,
                        UserType = model.UserType,
                        FileName = string.IsNullOrEmpty(fileName) ? null : ("/Upload/Patient_Images/" + fileName),
                        Images = data,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Title = model.TitleUser,
                        Email = model.Email
                    };
                    var user =  _userService.CreateOtherUser(dataModel);
                    if (checkDoctorExist.Status)
                    {
                        TempData[GlobalConstant.ErrorTemp] = GlobalConstant.EmailInviteExistUser;
                        return RedirectToAction("Profile", new { id = user.DoctorId });
                    }
                    TempData[GlobalConstant.ErrorTemp] = GlobalConstant.EmailInviteNewUser;
                    return RedirectToAction("Profile", new { id = user.DoctorId });

                }
                catch (ApiException ex)
                {
                    TempData[GlobalConstant.ErrorTemp] = ex.Message;
                    TempData[GlobalConstant.UserModel] = model;
                    return RedirectToAction("Add");
                }
            }
            else
            {
                TempData[GlobalConstant.ErrorTemp] = GlobalConstant.SessionExpiredOrNotLogin;
                return RedirectToAction("Index", "Home");
            }
        }

        [AllowAnonymous]
        public ActionResult ActiveInviteUser(string email, string hash, string clinicId, string clinicName)
        {
            try
            {
                _userService.ActiveUserInvite(email, hash, clinicId);
                return RedirectToAction("AccountExited", new { email, hash, clinicId, clinicName });
            }
            catch (ApiException)
            {
                return RedirectToAction("LinkExpired");
            }
        }

        [AllowAnonymous]
        public ActionResult Invite(string email, string hash, string clinicId)
        {
            return View();
        }


        [AllowAnonymous]
        public ActionResult InviteUser(string lang, string hash, string email, string clinicId, bool isExist)
        {
            //var select = new SelectList(Enum.GetValues(typeof(Title)).Cast<Title>().Select(v => new SelectListItem
            //{
            //    Text = v.DisplayName(),
            //    Value = v.ToString(),
            //}), "Value", "Text", "Dr");
            //ViewBag.listUserTitle = select;

            //try
            //{
            //    UserModel user = _userService.CheckExpiredAndGetUser(email, hash);

            //    HospitalModel clinic = _userService.GetClinicInforForPartner(lang, clinicId);
            //    if (isExist)
            //    {
            //        if (user.Status == 1)
            //        {
            //            return RedirectToAction("AccountExited", new { clinic = clinic.ClinicName, email, hash });
            //        }
            //    }
            //    ViewBag.clinicName = clinic.ClinicName;
            //    user.Hash = hash;
            //    InviteNewUserModel inu = new InviteNewUserModel();
            //    inu.User = user;
            //    return View(inu);
            //}
            //catch (Exception ex)
            //{
            //    //return RedirectToAction("AccountExpired", "Clinic");
            //    TempData[GlobalConstant.ErrorTemp] = ex.Message;
            //    return View(new InviteNewUserModel());
            //}
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult InviteUser(InviteNewUserModel model)
        {
            //try
            //{
            //    var dataurl = Request["image-data"];
            //    dataurl = dataurl.Replace("data:image/png;base64,", "");
            //    byte[] data = Convert.FromBase64String(dataurl);
            //    var fileName = Request["image-name"];
            //    if (data.Length > 0)
            //    {
            //        model.User.FileName = fileName;
            //        model.User.imagefile = data;
            //    }
            //    model.User.Login = true;
            //    model.Password.OldPassword = "";

            //    //model.Login = true;
            //    _userService.ConfirmInviteNewUser(BaseApiHeader, model);
            //    ViewBag.userEmail = model.User.Email;
            //    ViewBag.userPassword = model.Password.Password;
            //    ViewBag.inviteSuccess = true;
            //    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            //}
            //catch (Exception ex)
            //{
            //    return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            //}
            return View();
        }

        [AllowAnonymous]
        public ActionResult AccountExited(string clinicId, string email, string hash, string clinicName)
        {
            ViewBag.clinicName = clinicName;
            ViewBag.emaiName = email;
            return View();
        }

        [AllowAnonymous]
        public ActionResult LinkExpired()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult VerifyAccount(string hash, string email)
        {
            try
            {
                _userService.ConfirmInviteUser(BaseApiHeader, hash, email);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        //[HttpPost]
        public ActionResult SignIn(LoginViewModel model)
        {
            try
            {
                var loginModel = new LoginModel
                {
                    Email = model.Email,
                    AppType = "web",
                    Language = "en",
                    Password = model.Password,
                    TimeZone = model.TimeZone,
                    UserTypes = new List<int> { 2, 99 }
                };
                var apiHeader = _userService.Login(loginModel);
                if (apiHeader.User != null)
                {
                    apiHeader.User.FirstName = ConvertToUnSign2(apiHeader.User.FirstName);
                    apiHeader.User.LastName = ConvertToUnSign2(apiHeader.User.LastName);
                }
                if (apiHeader.Hospitals != null && apiHeader.Hospitals.Count > 0)
                {
                    foreach (var h in apiHeader.Hospitals)
                    {
                        h.Name = ConvertToUnSign2(h.Name);
                    }
                }
                apiHeader.TimeOffset = Utils.TimeZoneToOffset("+07:00");//model.TimeZone);
                apiHeader.Hospitals = apiHeader.Hospitals?.Where(x => x.Status == 1).ToList();

                var result = new LoginResultViewModel
                {
                    IsSuccess = true,
                    UserType = apiHeader.UserType,
                    Hospitals = apiHeader.Hospitals
                };
                var oldCookie = HttpContext.Request.Cookies[CookieConstant.ApiHeader];
                if (oldCookie != null)
                {
                    oldCookie.Expires = DateTime.Now.AddDays(-1);
                    HttpContext.Response.Cookies.Add(oldCookie);
                }

                var oldCookieHospital = HttpContext.Request.Cookies[CookieConstant.HospitalSelected];
                if (oldCookieHospital != null)
                {
                    oldCookieHospital.Expires = DateTime.Now.AddDays(-1);
                    HttpContext.Response.Cookies.Add(oldCookieHospital);
                }

                if (apiHeader.Hospitals != null && apiHeader.Hospitals.Count > 0)
                {

                    var cookieHospital = new HttpCookie(CookieConstant.HospitalSelected)
                    {
                        Value = JsonConvert.SerializeObject(apiHeader.Hospitals[0]),
                        Expires = Utils.UnixTimeStampToDateTime(apiHeader.SessionExpired)
                    };
                    HttpContext.Response.Cookies.Add(cookieHospital);
                    apiHeader.HospitalSelected = apiHeader.Hospitals[0].HospitalId;
                    apiHeader.HospitalSubType = apiHeader.Hospitals[0].SubType;
                    apiHeader.HospitalRoles = apiHeader.Hospitals[0].Roles;
                    apiHeader.HospitalName = (apiHeader.Hospitals[0].Name);
                }
                //if (apiHeader.User != null)
                //    apiHeader.User.DoctorInfos = null;

                var cookie = new HttpCookie(CookieConstant.ApiHeader)
                {
                    Value = JsonConvert.SerializeObject(apiHeader),
                    Expires = Utils.UnixTimeStampToDateTime(apiHeader.SessionExpired)
                };
                cookie.Value = ConvertToUnSign2(cookie.Value);
                HttpContext.Response.Cookies.Add(cookie);
                HttpContext.Response.Cookies.Add(new HttpCookie("timezone")
                {
                    Value = model.TimeZone,
                    Expires = Utils.UnixTimeStampToDateTime(apiHeader.SessionExpired)
                });
                if (apiHeader.Hospitals != null && apiHeader.Hospitals.Count == 0)
                {
                    RemoveCookie();
                }
                //return Json(result, JsonRequestBehavior.AllowGet);
                return RedirectToAction("Index", "Appointment");
            }
            catch (ApiException ex)
            {
                if (ex.ErrorCode.Equals("3"))
                {
                    return Json(new LoginResultViewModel { IsSuccess = false, Error = "Account not found. Please try again!" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new LoginResultViewModel { IsSuccess = false, Error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public string ConvertToUnSign2(string s)
        {
            var stFormD = s.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (char t in stFormD)
            {
                System.Globalization.UnicodeCategory uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(t);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(t);
                }
            }
            sb = sb.Replace('Đ', 'D');
            sb = sb.Replace('đ', 'd');
            return (sb.ToString().Normalize(NormalizationForm.FormD));
        }

        [HttpGet]
        public ActionResult SelectHospital(string id)
        {
            //try
            //{
            //    var httpCookie = HttpContext.Request.Cookies[CookieConstant.ApiHeader];
            //    if (httpCookie == null)
            //        return Json(new KeyValueResult(false, "Please login"), JsonRequestBehavior.AllowGet);

            //    var apiHeader = JsonConvert.DeserializeObject<ApiHeader>(httpCookie.Value);

            //    var hospital = apiHeader.Hospitals.FirstOrDefault(x => x.HospitalId == id);
            //    if (hospital == null)
            //        return Json(new KeyValueResult(false, "Clinic not exist"), JsonRequestBehavior.AllowGet);
            //    apiHeader.HospitalSelected = hospital.HospitalId;
            //    apiHeader.HospitalSubType = hospital.SubType;
            //    apiHeader.HospitalRoles = hospital.Roles;
            //    apiHeader.HospitalName = hospital.Name;
            //    var oldCookieHospital = HttpContext.Request.Cookies[CookieConstant.HospitalSelected];
            //    if (oldCookieHospital != null)
            //    {
            //        oldCookieHospital.Expires = DateTime.Now.AddDays(-1);
            //        HttpContext.Response.Cookies.Add(oldCookieHospital);
            //    }

            //    var cookieHospital = new HttpCookie(CookieConstant.HospitalSelected)
            //    {
            //        Value = JsonConvert.SerializeObject(hospital),
            //        Expires = Utils.UnixTimeStampToDateTime(apiHeader.SessionExpired)
            //    };

            //    if (apiHeader.User != null)
            //        apiHeader.User.DoctorInfos = null;
            //    var cookie = new HttpCookie(CookieConstant.ApiHeader)
            //    {
            //        Value = JsonConvert.SerializeObject(apiHeader),
            //        Expires = Utils.UnixTimeStampToDateTime(apiHeader.SessionExpired)
            //    };
            //    HttpContext.Response.Cookies.Add(cookie);
            //    HttpContext.Response.Cookies.Add(cookieHospital);
            //    return Json(new KeyValueResult(true, "Clinic not exist"), JsonRequestBehavior.AllowGet);
            //}
            //catch (Exception)
            //{
            //    return Json(new KeyValueResult(false, "Please try again"), JsonRequestBehavior.AllowGet);
            //}
            if (Session["auth_info"] != null)
            {
                var authInfo = (AuthInfo)Session["auth_info"];
                for (int i = 0; i < authInfo.DoctorClinics.Count(); i++)
                {
                    if (authInfo.DoctorClinics[i].Clinic.ClinicId == id)
                    {
                        authInfo.CurrentSelectedClinic = authInfo.DoctorClinics[i].Clinic;
                        break;
                    }
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult SignOut()
        {
            //RemoveCookie();
            //return RedirectToAction("Index", "Home");
            Session["auth_info"] = null;
            Session["token"] = null;
            return Redirect("https://login.windows.net/common/oauth2/logout?post_logout_redirect_uri=" + GlobalConstant.BaseUrl);
        }

        public void RemoveCookie()
        {
            var oldCookie = HttpContext.Request.Cookies[CookieConstant.ApiHeader];
            if (oldCookie != null)
            {
                oldCookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Response.Cookies.Add(oldCookie);
            }

            var oldCookieHospital = HttpContext.Request.Cookies[CookieConstant.HospitalSelected];
            if (oldCookieHospital != null)
            {
                oldCookieHospital.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Response.Cookies.Add(oldCookieHospital);
            }

            var doctorList = HttpContext.Request.Cookies[CookieConstant.DoctorList];
            if (doctorList != null)
            {
                doctorList.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Response.Cookies.Add(doctorList);
            }

            var priceList = HttpContext.Request.Cookies[CookieConstant.Pricing];
            if (priceList != null)
            {
                priceList.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Response.Cookies.Add(priceList);
            }
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword(string email)
        {
            try
            {
                _userService.ForgotPassword(email);
                return Json(new KeyValueResult(true), JsonRequestBehavior.AllowGet);
            }
            catch (ApiException ex)
            {
                return Json(new KeyValueResult(false, ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string lang, string email, string hash)
        {
            RemoveCookie();
            return RedirectToAction("ResetPasswords", new { lang, email, hash });
        }

        [AllowAnonymous]
        public ActionResult ResetPasswords(string lang, string email, string hash)
        {
            try
            {
                _userService.CheckExpired(email, hash);
                return View(new CreateNewPasswordModel()
                {
                    Email = email,
                    Hash = hash,
                    OldPassword = string.Empty
                });
            }
            catch (ApiException)
            {
                return RedirectToAction("ResendPasswordLink");
            }
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult CreatePassword(CreateNewPasswordModel model)
        {
            //try
            //{
            //    var apiHeader = _userService.CheckExpired(model.Email, model.Hash, true, model.Password);
            //    var cookieHospital = new HttpCookie(CookieConstant.HospitalSelected)
            //    {
            //        Value = JsonConvert.SerializeObject(apiHeader.Hospitals?[0]),
            //        Expires = Utils.UnixTimeStampToDateTime(apiHeader.SessionExpired)
            //    };
            //    HttpContext.Response.Cookies.Add(cookieHospital);
            //    if (apiHeader.Hospitals != null && apiHeader.Hospitals.Count > 0)
            //    {
            //        apiHeader.HospitalSelected = apiHeader.Hospitals[0].HospitalId;
            //        apiHeader.HospitalSubType = apiHeader.Hospitals[0].SubType;
            //        apiHeader.HospitalRoles = apiHeader.Hospitals[0].Roles;
            //        apiHeader.HospitalName = apiHeader.Hospitals[0].Name;
            //    }

            //    if (apiHeader.User != null)
            //        apiHeader.User.DoctorInfos = null;
            //    var cookie = new HttpCookie(CookieConstant.ApiHeader)
            //    {
            //        Value = JsonConvert.SerializeObject(apiHeader),
            //        Expires = Utils.UnixTimeStampToDateTime(apiHeader.SessionExpired)
            //    };
            //    HttpContext.Response.Cookies.Add(cookie);
            //    return Json(new KeyValueResult(true, apiHeader.Hospitals), JsonRequestBehavior.AllowGet);
            //}
            //catch (ApiException exception)
            //{
            //    return Json(new KeyValueResult(false, exception.Message), JsonRequestBehavior.AllowGet);
            //}
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResendPasswordLink()
        {
            return View();
        }


        public ActionResult CloseUnconfirmAccount(string userId, string hospitalId = "")
        {
            try
            {
                _userService.CloseUnconfirmAccount(BaseApiHeader, userId,
                    !string.IsNullOrEmpty(hospitalId) ? hospitalId : BaseApiHeader.HospitalSelected);
                return Json(new KeyValueResult(true), JsonRequestBehavior.AllowGet);
            }
            catch (ApiException ex)
            {
                return Json(new KeyValueResult(false, ex.Message), JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult ReopenAccount(string userId, string hospitalId = "")
        {
            try
            {
                _userService.ReOpenAccount(BaseApiHeader, userId,
                    !string.IsNullOrEmpty(hospitalId) ? hospitalId : BaseApiHeader.HospitalSelected);
                return Json(new KeyValueResult(true), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new KeyValueResult(false, ex.Message), JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult ClinicUsers(string keyword = "")
        {
            ViewBag.idSelected = "clinicsUsers";
            return View();
        }

        public ActionResult ListUsers(JQueryDataTableParamModel param)
        {
            try
            {
                var users = _userService.ListClinicUsers(param.iDisplayStart, param.iDisplayLength, param.sSearch);
                foreach (var user in users)
                {
                    user.Status = ((Status)user.Actived).DescriptionAttr();
                    if (user.ClinicUsers.Any())
                        foreach (var item in user.ClinicUsers)
                        {
                            item.UserTypeStr = ((UserType)item.UserType).DescriptionAttr();
                            item.AccessTypeStr = ((UserRole)item.AccessType).DescriptionAttr();
                            item.ClinicUserActivedStr = ((Status)item.ClinicUserActived).DescriptionAttr();
                        }
                }
                return Json(new
                {
                    draw = param.sEcho,
                    recordsTotal = users.Count,
                    recordsFiltered = _userService.Total(),
                    data = users
                }, JsonRequestBehavior.AllowGet);
            }
            catch (ApiException)
            {
                return Json(new
                {
                    draw = param.sEcho,
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    data = new List<UserModel>()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        //[AuthorCustom(new UserRole[] { UserRole.RoleHospitalAdmin, UserRole.RoleAdmin })]
        public ActionResult GetUser(string id)
        {
            try
            {
                var user = _userService.GetProfile(BaseApiHeader, id);
                return Json(new
                {
                    data = user
                }, JsonRequestBehavior.AllowGet);
            }
            catch (ApiException)
            {
                return Json(new
                {
                    data = new UserModel()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Details(string id, string hospitalId)
        {
            ViewBag.idSelected = "clinicsUsers";
            try
            {
                UserModel user = _userService.GetUserDetails(id);
                ViewBag.hospital = user.ClinicUsers.FirstOrDefault(x => x.Clinic.ClinicId == hospitalId);

                int userRole =
                    Array.Find(user.ClinicUsers, a => a.Clinic.ClinicId == hospitalId).AccessType;
                int userType =
                    Array.Find(user.ClinicUsers, a => a.Clinic.ClinicId == hospitalId).UserType;

                ViewBag.userRole = userRole;
                ViewBag.userType = userType;

                return View(user);
            }
            catch (Exception)
            {
                return View();
            }
        }
        public ActionResult EditDetails(string id, string hospitalId)
        {
            ViewBag.idSelected = "clinicsUsers";
            UserModel user = _userService.GetUserDetails(id);
            if (user.ClinicUsers.FirstOrDefault(x => x.Clinic.ClinicId == hospitalId).ClinicUserActived == (int)Status.Closed)
            {
                return RedirectToAction("Details", new { id, hospitalId });
            }
            var hospital = user.ClinicUsers.FirstOrDefault(x => x.Clinic.ClinicId == hospitalId);
            ViewBag.hospital = hospital;
            ViewBag.countClinic = user.ClinicUsers.Length;
            if (hospital != null)
            {
                int userRole =
                    hospital.AccessType;
                int userType =
                    hospital.UserType;

                ViewBag.listUserRole =
                    new SelectList(Enum.GetValues(typeof(UserRole)).Cast<UserRole>().Select(v => new SelectListItem
                    {
                        Text = v.DisplayName(),
                        Value = ((int)v).ToString(),
                    }).Where(v => Int32.Parse(v.Value) != 0), "Value", "Text", userRole);

                ViewBag.listUserType =
                    new SelectList(Enum.GetValues(typeof(UserType)).Cast<UserType>().Select(v => new SelectListItem
                    {
                        Text = v.DisplayName(),
                        Value = ((int)v).ToString(),
                    }).Where(v => Int32.Parse(v.Value) != 0), "Value", "Text", userType);

                ViewBag.UserSubType = userType;
                ViewBag.UserRole = userRole;
            }
            ViewBag.listUserTitle =
                  new SelectList(Enum.GetValues(typeof(Title)).Cast<Title>().Select(v => new SelectListItem
                  {
                      Text = v.DescriptionAttr(),
                      Value = v.ToString(),
                  }), "Value", "Text", user.TitleUser ?? "Mr");
            ViewBag.Title = user.TitleUser;
            user.AccessType = hospital.AccessType;
            user.UserType = hospital.UserType;
            return View(user);
        }

        [HttpPost]
        public ActionResult EditDetails(UserModel model, string hospitalId, string clinicUserId)
        {
            try
            {
                var dataurl = Request["image-data"];
                dataurl = dataurl.Replace("data:image/png;base64,", "");
                byte[] data = Convert.FromBase64String(dataurl);
                var fileName = Request["image-name"];
                if (data.Length > 0)
                {
                    model.FileName = fileName;
                    model.Imagefile = data;
                }
                var dataModel = new UpdateClinicUserModel
                {
                    ClinicUserId = clinicUserId,
                    DoctorId = model.DoctorId,
                    AccessType = model.AccessType,
                    UserType = model.UserType,
                    FileName = string.IsNullOrEmpty(fileName) ? null : ("/Upload/Patient_Images/" + fileName),
                    Imagefile = data,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Title = model.TitleUser
                };
                _userService.UpdateOtherUser(dataModel);
                return RedirectToAction("Details", new { id = model.DoctorId, hospitalId });
            }
            catch (Exception)
            {
                return RedirectToAction("EditDetails", new { id = model.DoctorId, hospitalId });
            }
        }

        [HttpPost]
        public ActionResult CheckUpcomingAppointmentsOfDoctor(string doctorId, string clinicId, int status)
        {
            if (status == (int)Status.Closed)
            {
                var timeZone = Request.Cookies["TimeZone"].Value;
                var timeCurrent = Utils.GetMidnightCurrentDateTimeSpan(timeZone);
                StatusResult result = _userService.CheckUpcomingAppointmentsOfDoctor(doctorId, clinicId, timeCurrent);
                if (result.Status)
                {
                    return Json(new KeyValueResult(false, "This doctor have " + result.Total + " upcoming appointments with this clinic. If you close this account, these appointments will be cancelled and the patients will be notifed."), JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new KeyValueResult(true, null), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ActiveUser(string doctorId, int status, string clinicId)
        {
            if (Session["auth_info"] != null)
            {
                try
                {
                    _userService.ActiveUser(doctorId, status, clinicId);
                    return Json(new KeyValueResult(true, (status == 0) ? "User successfully blocked." : "User successfully actived."), JsonRequestBehavior.AllowGet);
                }
                catch (ApiException ex)
                {
                    return Json(new KeyValueResult(false, ex.Message), JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                TempData[GlobalConstant.ErrorTemp] = GlobalConstant.SessionExpiredOrNotLogin;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult ChangeStatusUser(string email, string clinicId, int status, string doctorId, int isUnconfirmed)
        {
            if (Session["auth_info"] != null)
            {
                try
                {
                    var authInfo = (AuthInfo)Session["auth_info"];
                    var timeZone = Request.Cookies["TimeZone"].Value;
                    var timeCurrent = Utils.GetMidnightCurrentDateTimeSpan(timeZone);
                    var otherAccount = doctorId != authInfo.DoctorId ? 1 : 0;
                    var statusType = isUnconfirmed == 1 ? "CLOSEUNCONFIRMED" : ((status == (int)Status.Closed) ? "CLOSE" : "REOPEN");
                    _userService.ChangeStatusUser(email, clinicId, otherAccount, timeCurrent, status, statusType);
                    return Json(new KeyValueResult(true, new { Message = (isUnconfirmed == 1 || status == (int)Status.Closed) ? "Clinic user successfully closed." : "Clinic user successfully active.", IsOtherAccount = otherAccount }), JsonRequestBehavior.AllowGet);
                }
                catch (ApiException ex)
                {
                    return Json(new KeyValueResult(false, ex.Message), JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                TempData[GlobalConstant.ErrorTemp] = GlobalConstant.SessionExpiredOrNotLogin;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult CreateOffice365User(string doctorId, string email, string email365, string firstname, string lastname)
        {
            try
            {
                var token = Session["token"];
                var displayName = firstname + " " + lastname;
                if (token == null)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                var password365 = "Welio" + DateTime.Now.Year;
                var rs = _userService.CreateOffice365User(email365, displayName, password365, token.ToString());
                if (rs == "ok")
                {
                    //_userService.SendMailAccount365(email, firstname, email365, password365);
                    _userService.UpdateAccountOffice365(doctorId, email365, password365);
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                return Json(new KeyValueResult(false, rs), JsonRequestBehavior.AllowGet);
            }
            catch (ApiException)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult SendEmailOffice365Account(string email, string email365, string firstname, string password365)
        {
            try
            {
                _userService.SendMailAccount365(email, firstname, email365, password365);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (ApiException)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateOffice365(string doctorId, string hospitalId, string email365)
        {
            ViewBag.DoctorId = doctorId;
            ViewBag.Email365 = email365;
            ViewBag.HospitalId = hospitalId;
            return View();
        }

        [HttpPost]
        public ActionResult UpdateOffice365Email(string doctorId, string hospitalId, string email365)
        {
            try
            {
                _userService.UpdateAccountOffice365(doctorId, email365, null);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (ApiException ex)
            {
                return Json(new KeyValueResult(false, ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SendEmailOffice365(string email, string email365, string firstname, string hospitalId, string id)
        {
            ViewBag.Email = email;
            ViewBag.Email365 = email365;
            ViewBag.FirstName = firstname;
            ViewBag.HospitalId = hospitalId;
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public ActionResult SendAccountOffice365(string email, string email365, string firstname, string password365)
        {
            try
            {
                _userService.SendMailAccount365(email, firstname, email365, password365);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (ApiException ex)
            {
                return Json(new KeyValueResult(false, ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
    }
}