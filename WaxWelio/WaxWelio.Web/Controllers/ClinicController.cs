using System;
using System.Collections.Generic;
using System.Linq;
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
using WaxWelio.Entities.Result;
using WaxWelio.Entities;

namespace WaxWelio.Web.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class ClinicController : BaseController
    {
        private readonly IHospitalService _clinicService;
        private readonly IClinicService _clinicService2;
        private readonly IUserService _userService;

        public ClinicController(IHospitalService clinicService, IUserService userService, IClinicService clinicService2)
        {
            _clinicService = clinicService;
            _userService = userService;
            _clinicService2 = clinicService2;
        }
        //[AuthorCustom(UserRole.RoleAdmin)]
        public ActionResult NewClinic()
        {
            ViewBag.idSelected = "newClinics";
            return View();
        }
        //[AuthorCustom(UserRole.RoleAdmin)]
        public ActionResult ActiveClinics()
        {
            ViewBag.idSelected = "activeClinics";
            return View();
        }
        //[AuthorCustom(UserRole.RoleAdmin)]
        public ActionResult AllClinics()
        {
            ViewBag.idSelected = "allClinics";
            return View();
        }

        public ActionResult ListClinics(string type, string sSearch, JQueryDataTableParamModel param)
        {
            try
            {
                var hospitals = _clinicService2.GetListClinic(type, param.sSortDir_0, sSearch, param.iDisplayStart, param.iDisplayLength);
                var index = 1;
                if (param.iDisplayStart > 0)
                    index += param.iDisplayStart;
                foreach (var hospital in hospitals)
                {
                    hospital.No = index;
                    hospital.StatusName = Enum.Parse(typeof(ClinicStatus), hospital.Actived.ToString()).DescriptionAttr();
                    hospital.Created = ParseStrDate(hospital.Created);
                    index++;
                }
                return Json(new
                {
                    draw = param.sEcho,
                    recordsTotal = hospitals.Count,
                    recordsFiltered = _clinicService2.Total(),
                    data = hospitals
                }, JsonRequestBehavior.AllowGet);
            }
            catch (ApiException)
            {
                return Json(new
                {
                    draw = param.sEcho,
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    data = new List<ClinicResult>()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult LoginClinic(string id)
        {
            try
            {
                var clinic = _clinicService.GetById(BaseApiHeader, id);
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
                var newApiHeader = BaseApiHeader;
                newApiHeader.HospitalSelected = clinic.Id;
                newApiHeader.HospitalRoles = new List<int> { (int)UserRole.Administrator };
                newApiHeader.HospitalSubType = (int)UserType.PracticeManager;
                newApiHeader.HospitalName = clinic.ClinicName;
                //if (newApiHeader.User != null)
                //    newApiHeader.User.DoctorInfos = null;
                var cookie = new HttpCookie(CookieConstant.ApiHeader)
                {
                    Value = JsonConvert.SerializeObject(newApiHeader),
                    Expires = Utils.UnixTimeStampToDateTime(newApiHeader.SessionExpired)
                };
                HttpContext.Response.Cookies.Add(cookie);

                var hospitalCookie = new HttpCookie(CookieConstant.HospitalSelected)
                {
                    Value = JsonConvert.SerializeObject(clinic),
                    Expires = Utils.UnixTimeStampToDateTime(newApiHeader.SessionExpired)
                };
                HttpContext.Response.Cookies.Add(hospitalCookie);
                return RedirectToAction("Index", "Appointment");
            }
            catch (ApiException)
            {
                return RedirectToAction("ActiveClinics");
            }
        }

        //[AuthorCustom(UserRole.RoleAdmin)]
        public ActionResult ExitClinic()
        {
            var clinicId = BaseApiHeader.HospitalSelected;
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
            var newApiHeader = BaseApiHeader;
            newApiHeader.HospitalSelected = string.Empty;
            newApiHeader.HospitalRoles = null;
            newApiHeader.HospitalSubType = 0;
            newApiHeader.HospitalName = string.Empty;
            //if (newApiHeader.User != null)
            //    newApiHeader.User.DoctorInfos = null;
            var cookie = new HttpCookie(CookieConstant.ApiHeader)
            {
                Value = JsonConvert.SerializeObject(newApiHeader),
                Expires = Utils.UnixTimeStampToDateTime(newApiHeader.SessionExpired)
            };

            HttpContext.Response.Cookies.Add(cookie);

            return RedirectToAction("ClinicDetails",new { id=clinicId, selected="allClinics"});
        }

        // GET: Clinic
        //[AuthorCustom(UserRole.RoleHospitalAdmin)]
        public ActionResult Index()
        {
            if (Session["auth_info"] != null)
            {
                try
                {
                    ViewBag.idSelected = "clinic";
                    var authInfo = (AuthInfo)Session["auth_info"];
                    var clinic = _clinicService2.GetDetails(authInfo.CurrentSelectedClinic.ClinicId);
                    return View(clinic);
                }
                catch (ApiException ex)
                {
                    TempData[GlobalConstant.ErrorTemp] = ex.Message;
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData[GlobalConstant.ErrorTemp] = GlobalConstant.SessionExpiredOrNotLogin;
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Details()
        {
            if (Session["auth_info"] != null)
            {
                try
                {
                    ViewBag.idSelected = "clinic";
                    var authInfo = (AuthInfo)Session["auth_info"];
                    var clinic = _clinicService2.GetDetails(authInfo.CurrentSelectedClinic.ClinicId);
                    return View(clinic);
                }
                catch (Exception)
                {
                    return View();
                }
            }
            else
            {
                TempData[GlobalConstant.ErrorTemp] = GlobalConstant.SessionExpiredOrNotLogin;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult Details(ClinicResult model)
        {
            try
            {
                _clinicService2.Update(ToClinicModel(model));
                var authInfo = (AuthInfo)Session["auth_info"];
                authInfo.CurrentSelectedClinic = _clinicService2.GetDetails(authInfo.CurrentSelectedClinic.ClinicId);
                Session["authInfo"] = authInfo;
                return RedirectToAction("Index", "Clinic");
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("FAILURE_ADDRESS_EXISTED"))
                {
                    TempData[GlobalConstant.ErrorTemp] = GlobalConstant.ClinicAddressExisted;
                }
                else
                {
                    TempData[GlobalConstant.ErrorTemp] = ex.Message;
                }
                return View(model);
            }
        }

        [AllowAnonymous]
        public ActionResult SignUp()
        {
            return View(new SignUpModel());
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult SignUp(SignUpModel model)
        {
            try
            {
                if (!ModelState.IsValid) return View(model);
                _clinicService.SignUp(ToTrimSignUpModel(model));
                return RedirectToAction("SignUpSuccess", new { fullName = model.DoctorFirstName + " " + model.DoctorLastName });
            }
            catch (ApiException ex)
            {
                ViewBag.ErrorSignUp = ex.Message;

                return View(model);
            }
        }

        [AllowAnonymous]
        public ActionResult SignUpSuccess(string fullName)
        {
            ViewBag.FullName = fullName;
            return View();
        }

        [AllowAnonymous]
        public ActionResult ActiveClinic(string lang, string hash, string email)
        {
            RemoveCookie();
            return RedirectToAction("ActiveClinicUser", new {lang, hash, email});
        }

        [AllowAnonymous]
        public ActionResult ActiveClinicUser(string lang, string hash, string email)
        {
            try
            {
                _clinicService.CheckExpired(BaseApiHeader, hash, email);
                return RedirectToAction("CreatePassword", new { hash, email });
            }
            catch (ApiException ex)
            {
                if (ex.ErrorCode.Equals("133"))
                {
                    return RedirectToAction("LinkUsed");
                }
                return RedirectToAction("AccountExpired");
                //return View();
            }
        }

        [AllowAnonymous]
        public ActionResult LinkUsed()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult CreatePassword(string hash, string email)
        {
            ViewBag.hash = hash;
            ViewBag.email = email;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult CreatePassword(CreateNewPasswordModel model)
        {
            try
            {
                if (!ModelState.IsValid) return View(model);
                model.OldPassword = "";
                ApiHeader apiHeader = _clinicService.ActiveUser(BaseApiHeader, model);

                apiHeader.Hospitals = apiHeader.Hospitals?.Where(x => x.Status == 1).ToList();
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
                    apiHeader.HospitalName = apiHeader.Hospitals[0].Name;
                }
                //if (apiHeader.User != null)
                //    apiHeader.User.DoctorInfos = null;
                var cookie = new HttpCookie(CookieConstant.ApiHeader)
                {
                    Value = JsonConvert.SerializeObject(apiHeader),
                    Expires = Utils.UnixTimeStampToDateTime(apiHeader.SessionExpired)
                };
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
                return RedirectToAction("Index", "Appointment");
                //model.OldPassword = "";
                //var apiHeader = new ApiHeader();
                //apiHeader.UserId = result[1].Value.ToString();
                //apiHeader.SessionId = result[0].Value.ToString();
                //_clinicService.CreatePassword(apiHeader, model);
                //ViewBag.email = model.Email;
                //ViewBag.password = model.Password;
                //ViewBag.createPass = "true";
                //return View();
            }
            catch (ApiException)
            {
                //TempData[GlobalConstant.ErrorTemp] = ex.Message;
                // ignored
                return RedirectToAction("CreatePasswordError");
            }

        }

        [AllowAnonymous]
        public ActionResult CreatePasswordError()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult AccountExpired()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ResendEmail(string email)
        {
            try
            {
                _clinicService.ResendActiveEmail(BaseApiHeader, email);
                return Json(new { success = "true" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { success = "false" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ResendInviteEmail(string userId, string clinicId)
        {
            try
            {
                _clinicService.ResendInviteEmail(userId, clinicId);
                return Json(new KeyValueResult(true), JsonRequestBehavior.AllowGet);
            }
            catch (ApiException ex)
            {
                return Json(new KeyValueResult(false, ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        public ActionResult NotifyClinic()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult NotifyClinic(NotifyClinicModel model)
        {
            try
            {
                _clinicService.NotifyClinic(null, model);
                return RedirectToAction("NotifyComplete");
            }
            catch (Exception)
            {
                return View();
            }
        }

        [AllowAnonymous]
        public ActionResult NotifyComplete()
        {
            return View();
        }

        public ActionResult ClinicDetails(string id, string selected)
        {
            try
            {
                var clinic = _clinicService2.GetDetails(id);
                clinic.Created = ParseStrDate(clinic.Created);
                ViewBag.idSelected = selected;
                return View(clinic);
            }
            catch (Exception)
            {
                return View();
            }
        }

        public ActionResult Edit(string id, string selected)
        {
            ViewBag.idSelected = selected;
            try
            {
                var signUpStatus = from SignUpStatus d in Enum.GetValues(typeof(SignUpStatus))
                                   select new { Value = (int)d, Text = d.DescriptionAttr() };
                var clinic = _clinicService2.GetDetails(id);
                ViewBag.SignUpStatuss = new SelectList(signUpStatus, "Value", "Text", clinic.Actived);
                return View(clinic);
            }
            catch (Exception)
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Edit(ClinicResult hospital, string selected)
        {
            try
            {
                if (!ModelState.IsValid) return View(hospital);
                _clinicService2.Update(ToClinicModel(hospital));
                TempData[GlobalConstant.ErrorTemp] = "Clinic details successfully updated.";
                return RedirectToAction("ClinicDetails", new { id = hospital.ClinicId, selected });
            }
            catch (ApiException ex)
            {

                TempData[GlobalConstant.ErrorTemp] = ex.Message;
                ViewBag.idSelected = selected;
                var signUpStatus = from SignUpStatus d in Enum.GetValues(typeof(SignUpStatus))
                                   select new { Value = (int)d, Text = d.DescriptionAttr() };
                ViewBag.SignUpStatuss = new SelectList(signUpStatus, "Value", "Text", hospital.Actived);
                return View(hospital);
            }
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

        private ClinicModel ToClinicModel(ClinicResult model)
        {
            return model == null ? null : new ClinicModel
            {
                ClinicId = model.ClinicId,
                Status = model.Actived,
                ClinicName = model.ClinicName,
                ClinicEmail = model.ClinicEmail,
                Street1 = model.Street1,
                Street2 = model.Street2,
                State = model.State,
                PostCode = model.PostCode,
                Phone = model.Phone
            };
        }

        private SignUpModel ToTrimSignUpModel(SignUpModel model)
        {
            return model == null ? null : new SignUpModel
            {
                AddressLine1 = model.AddressLine1.Trim(),
                AddressLine2 = string.IsNullOrEmpty(model.AddressLine2) ? "" : model.AddressLine2.Trim(),
                ClinicName = model.ClinicName.Trim(),
                State = model.State.Trim(),
                PostCode = model.PostCode.Trim(),
                DoctorEmail = model.DoctorEmail.Trim(),
                DoctorFirstName = model.DoctorFirstName.Trim(),
                DoctorLastName = model.DoctorLastName.Trim(),
                DoctorPhoneNumber = model.DoctorPhoneNumber.Trim(),
                DoctorTitle = model.DoctorTitle.Trim(),
                Phone = model.Phone.Trim(),
            };
        }
    }
}