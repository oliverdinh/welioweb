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
using WaxWelio.Entities.ViewModels;
using WaxWelio.Entities;

namespace WaxWelio.Web.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class AppointmentController : BaseController
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;
        private readonly IPriceService _priceService;
        private readonly IUserService _userService;

        public AppointmentController(IAppointmentService appointmentService, IUserService userService,
            IPriceService priceService, IPatientService patientService)
        {
            _appointmentService = appointmentService;
            _userService = userService;
            _priceService = priceService;
            _patientService = patientService;
        }

        public ActionResult Index()
        {
            if (Session["auth_info"] != null)
            {
                try
                {
                    var authInfo = (AuthInfo)Session["auth_info"];
                    var doctor = _userService.GetDoctorsByClinic(authInfo.CurrentSelectedClinic.ClinicId)
                                    .Select(x => new KeyValueResult(x.DoctorId, x.FullName))
                                    .ToList();
                    var userType = authInfo.DoctorClinics.First(x => x.Clinic.ClinicId == authInfo.CurrentSelectedClinic.ClinicId).ClinicUserUserType;
                    var doctorWork = userType == (int)UserType .PracticeManager
                        ? new SelectList(doctor.ToList(), "Key", "Value") 
                        : (userType == (int)UserType.Doctor 
                                ? new SelectList(new List<KeyValueResult> { new KeyValueResult(authInfo.DoctorId, authInfo.FullName) }, "Key", "Value") 
                                : new SelectList(new List<KeyValueResult>(), "Key", "Value"));
                    var listPrices = _priceService.Get(authInfo.CurrentSelectedClinic.ClinicId);
                    var listPricing = new SelectList(listPrices.ToList(), "Duration", "Duration");
                    ViewBag.ListDoctor = doctorWork;
                    ViewBag.ListPrices = listPricing;
                    ViewBag.CanCreateAppoinment = true;
                    doctor.Insert(0, new KeyValueResult("all", "All Doctors"));
                    ViewBag.DoctorFilter = new SelectList(doctor, "Key", "Value");
                }
                catch (ApiException)
                {
                    return RedirectToAction("Index", "Home");
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult GetAppointment(JQueryDataTableParamModel param, string status, string doctorId, string selectDate, string keyword)
        {
            try
            {
                var authInfo = (AuthInfo)Session["auth_info"];
                if (authInfo == null)
                {
                    return Json(new KeyValueResult(false, GlobalConstant.SessionExpired), JsonRequestBehavior.AllowGet);
                }
                var timeZone = Request.Cookies["TimeZone"].Value;
                
                var filter = new FilterAppointmentModel
                {
                    ClinicId = authInfo.CurrentSelectedClinic.ClinicId,
                    DoctorId = string.IsNullOrEmpty(doctorId) ? "all" : doctorId,
                    SelectDate = string.IsNullOrEmpty(selectDate) ? "all" : selectDate,
                    Type = status,
                    Start = param.iDisplayStart,
                    Limit = param.iDisplayLength,
                    FromTime = string.IsNullOrEmpty(selectDate) ? Utils.GetMidnightCurrentDateTimeSpan(timeZone) : Utils.GetMidnightBeginDateTimeSpan(DateTime.Parse(selectDate), timeZone),
                    ToTime = string.IsNullOrEmpty(selectDate) ? 0 : Utils.GetMidnightEndDateTimeSpan(DateTime.Parse(selectDate), timeZone),
                    OrderByStartDateTime = string.IsNullOrEmpty(param.sSortDir_0) ? "ASC" : param.sSortDir_0.ToUpper(),
                    Keywords = string.IsNullOrEmpty(keyword) ? "" : keyword
                };
                var appointments = _appointmentService.GetListAppointment(filter);
                var index = 1;
                if (param.iDisplayStart > 0)
                {
                    index += param.iDisplayStart;
                }
                var appointmentListViewModel = new List<AppointmentListViewModel>();
                foreach (var item in appointments)
                {
                    var dateTime = Utils.FromUnixTime(item.ExpectedStartDateTime, timeZone);
                    var actualDateTime = (item.ActualStartDateTime == null)
                        ? (DateTime?)null
                        : Utils.FromUnixTime((long)item.ActualStartDateTime, timeZone);
                    //if (item.CallLog != null)
                    //{
                    //    foreach (var call in item.CallLog)
                    //    {

                    //        call.StrStatus = ((CallStatus)call.Status).DescriptionAttr();
                    //        var sDate = Utils.UnixTimeStampToDateTime(Int64.Parse(call.StartDate),
                    //            BaseApiHeader.TimeOffset);
                    //        call.StrStartDate = sDate.ToString(GlobalConstant.DateTimeFormat);
                    //    }
                    //}

                    appointmentListViewModel.Add(new AppointmentListViewModel
                    {
                        Id = item.AppointmentId,
                        Duration = item.ExpectedDuration,
                        Status = item.Status,
                        Date = dateTime.ToString(GlobalConstant.DateFormat),
                        No = index,
                        DoctorName = item.Doctor.FullName,
                        PatientName = item.Patient.FullName,
                        Time = dateTime.ToString("HH:mm"),
                        Phone = item.Patient.Phone,
                        SipUriMeeting = item.MeetingUri,
                        ActualDuration = (item.ActualDuration == null) ? "-" : item.ActualDuration.ToString(),
                        ActualFee = item.ActualFee == 0 ? "-" : "$" + item.ActualFee,
                        ActualStart = actualDateTime?.ToString("HH:mm") ?? "-",
                        ActualStatus = ((AppointmentStatus)item.Status).DescriptionAttr(),
                        AppointmentDate = dateTime.ToString(GlobalConstant.DateFormat),
                        Fee = "$" + item.ExpectedFee,
                        OtherFamilyMember = item.PatientFullName,
                        HasCarer = !string.IsNullOrEmpty(item.PatientFirstName),
                        FirstName = item.Patient.FirstName,
                        LastName = item.Patient.LastName,
                        OtherFirstName = item.PatientFirstName,
                        OtherLastName = item.PatientLastName,
                        IsFloating = item.Patient.IsFoalting == 1,
                        Email = item.Patient.Email,
                        DoctorId = item.Doctor.DoctorId,
                        PatientId = item.Patient.PatientId,
                        UrlJoinMeeting = item.JoinMettingUrl,
                        ExpectedStartDateTime = item.ExpectedStartDateTime,
                        PatientAvatar = item.Patient.PatientAvatar
                    });

                    index++;
                }
                var list = (from appointment in appointmentListViewModel
                            select new
                            {
                                appointment
                            }).ToList();
                return Json(new
                {
                    draw = param.sEcho,
                    recordsTotal = list.Count,
                    recordsFiltered = _appointmentService.Total(),
                    data = list
                }, JsonRequestBehavior.AllowGet);
            }
            catch (ApiException)
            {
                var appointmentListViewModel = new List<AppointmentListViewModel>();
                return Json(new
                {
                    draw = param.sEcho,
                    recordsTotal = appointmentListViewModel.Count,
                    recordsFiltered = _appointmentService.Total(),
                    data = appointmentListViewModel
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult CreateAppointment(CreateAppointmentViewModel model)
        {
            try
            {
                var timeZone = Request.Cookies["TimeZone"].Value;
                var authInfo = (AuthInfo)Session["auth_info"];
                if (authInfo == null)
                {
                    return Json(new KeyValueResult(false, "Your session has expired. Please signin again."), JsonRequestBehavior.AllowGet);
                }
                var dateTimeExpected = model.ExpectedDate + " " + model.ExpectedTime;
                var dateExpected = Utils.StringToDateTime(dateTimeExpected);
                var appointment = new CreateUpdateAppointmentModel
                {
                    ClinicId = authInfo.CurrentSelectedClinic.ClinicId,
                    PatientId = model.PatientId,
                    DoctorId = model.DoctorId,
                    DoctorName = model.DoctorName,
                    ClinicName = authInfo.CurrentSelectedClinic.ClinicName,
                    ClinicPhone = authInfo.CurrentSelectedClinic.Phone,
                    ExpectedDuration = int.Parse(model.Duration),
                    CarerFirstName = model.OtherFirstName,
                    CarerLastName = model.OtherLastName,
                    IsCarer = model.IsCarer ? 1: 0,
                    IsNewPatient = model.IsNewPatient,
                    Patient = new PatientCreateAppointmentModel
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Phone = model.Phone,
                            Email = model.Email
                        },
                    ExpectedStartDateTime = Utils.ToUTCTimeSpan(dateExpected, timeZone),
                    MeetingUri = model.SipUriMeeting,
                    JoinMettingUrl = model.UrlJoinMeeting
                };

                _appointmentService.CreateAppointment(appointment);
                return Json(new KeyValueResult(true, "Welio appointment successfully created."), JsonRequestBehavior.AllowGet);
            }
            catch (ApiException ex)
            {
                return Json(new KeyValueResult(false, ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult UpdateAppointment(CreateAppointmentViewModel model)
        {
            try
            {
                var timeZone = Request.Cookies["TimeZone"].Value;
                var authInfo = (AuthInfo)Session["auth_info"];
                if (authInfo == null)
                {
                    return Json(new KeyValueResult(false, "Your session has expired. Please signin again."), JsonRequestBehavior.AllowGet);
                }
                var dateTimeExpected = model.ExpectedDate + " " + model.ExpectedTime;
                var dateExpected = Utils.StringToDateTime(dateTimeExpected);
                var appointment = new CreateUpdateAppointmentModel
                {
                    AppointmentId = model.Id,
                    ClinicId = authInfo.CurrentSelectedClinic.ClinicId,
                    PatientId = model.PatientId,
                    DoctorId = model.DoctorId,
                    DoctorName = model.DoctorName,
                    ClinicName = authInfo.CurrentSelectedClinic.ClinicName,
                    ClinicPhone = authInfo.CurrentSelectedClinic.Phone,
                    ExpectedDuration = int.Parse(model.Duration),
                    CarerFirstName = model.OtherFirstName,
                    CarerLastName = model.OtherLastName,
                    IsCarer = model.IsCarer ? 1 : 0,
                    IsNewPatient = model.IsNewPatient,
                    Patient = new PatientCreateAppointmentModel
                    {
                        PatientId = model.PatientId,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Phone = model.Phone,
                        Email = model.Email
                    },
                    ExpectedStartDateTime = Utils.ToUTCTimeSpan(dateExpected, timeZone),
                    MeetingUri = model.SipUriMeeting,
                    JoinMettingUrl = model.UrlJoinMeeting,
                    IsFloating = model.IsFloating
                };

                _appointmentService.UpdateAppointment(appointment);
                return Json(new KeyValueResult(true, "Welio appointment successfully updated."), JsonRequestBehavior.AllowGet);
            }
            catch (ApiException ex)
            {
                return Json(new KeyValueResult(false, ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult CancelAppointment(CancelAppointmentViewModel model)
        {
            try
            {
                var authInfo = (AuthInfo)Session["auth_info"];
                if (authInfo == null)
                {
                    return Json(new KeyValueResult(false, "Your session has expired. Please signin again."), JsonRequestBehavior.AllowGet);
                }
                var data = new CancelAppoinmentModel
                {
                    AppointmentId = model.AppointmentId,
                    DoctorName = model.DoctorName,
                    ClinicName = authInfo.CurrentSelectedClinic.ClinicName,
                    ClinicPhone = authInfo.CurrentSelectedClinic.Phone,
                    Patient = new PatientCreateAppointmentModel
                    {
                        FirstName = model.PatientFirstName,
                        LastName = model.PatientLastName,
                        Phone = model.PatientPhone,
                        Email = model.PatientEmail
                    },
                    ExpectedStartDateTime = model.ExpectedStartDateTime,
                };

                _appointmentService.CancelAppointment(data);
                return Json(new KeyValueResult(true, "Welio appointment successfully canceled."), JsonRequestBehavior.AllowGet);
            }
            catch (ApiException ex)
            {
                return Json(new KeyValueResult(false, ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Cancellation()
        {
            if (Session["auth_info"] != null)
            {
                try
                {
                    var authInfo = (AuthInfo)Session["auth_info"];
                    var doctor = _userService.GetDoctorsByClinic(authInfo.CurrentSelectedClinic.ClinicId)
                                    .Select(x => new KeyValueResult(x.DoctorId, x.FullName))
                                    .ToList();
                    var accessType = authInfo.DoctorClinics.First(x => x.Clinic.ClinicId == authInfo.CurrentSelectedClinic.ClinicId).ClinicUserAccessType;
                    var userType = authInfo.DoctorClinics.First(x => x.Clinic.ClinicId == authInfo.CurrentSelectedClinic.ClinicId).ClinicUserUserType;
                    var doctorWork = accessType == (int)UserRole.Administrator
                        ? new SelectList(doctor.ToList(), "Key", "Value")
                        : (userType == (int)UserType.Doctor
                                ? new SelectList(new List<KeyValueResult> { new KeyValueResult(authInfo.DoctorId, authInfo.FullName) }, "Key", "Value")
                                : new SelectList(new List<KeyValueResult>(), "Key", "Value"));
                    var listPrices = _priceService.Get(authInfo.CurrentSelectedClinic.ClinicId);
                    var listPricing = new SelectList(listPrices.ToList(), "Duration", "Duration");
                    ViewBag.ListPrices = listPricing;
                    ViewBag.ListDoctor = doctorWork;
                    ViewBag.CanCreateAppoinment = true;
                    doctor.Insert(0, new KeyValueResult("all", "All Doctors"));
                    ViewBag.DoctorFilter = new SelectList(doctor, "Key", "Value");
                }
                catch (ApiException)
                {
                    return RedirectToAction("Index", "Home");
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [AllowAnonymous]
        public ActionResult GetPatientByPhone(string phone)
        {
            try
            {
                //phone = HttpUtility.UrlEncode(phone.Trim());
                var patient = _patientService.GetByPhone(phone);
                return Json(patient != null ? new KeyValueResult(true, patient) : new KeyValueResult(false),
                    JsonRequestBehavior.AllowGet);
            }
            catch (ApiException exception)
            {
                return Json(new KeyValueResult(false, exception.Message), JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        public ActionResult PushNotification(string id)
        {
            try
            {
             var callid =   _appointmentService.PushNotifiCall(BaseApiHeader, id);
                return Json(new KeyValueResult(true, callid), JsonRequestBehavior.AllowGet);
            }
            catch (ApiException e)
            {
                return Json(new KeyValueResult(false, e.Message), JsonRequestBehavior.AllowGet);
            }

        }

        [AllowAnonymous]
        public ActionResult FinishCall(string id)
        {
            try
            {
                _appointmentService.FinishCall(BaseApiHeader, id, 9);
                return Json(new KeyValueResult(true), JsonRequestBehavior.AllowGet);
            }
            catch (ApiException e)
            {
                return Json(new KeyValueResult(false, e.Message), JsonRequestBehavior.AllowGet);
            }

        }

        [AllowAnonymous]
        public ActionResult UpdateStatus(string id, int status)
        {
            if (Session["auth_info"] != null)
            {
                try
                {
                    _appointmentService.UpdateStatus(id, status);
                    return Json(new KeyValueResult(true, ""), JsonRequestBehavior.AllowGet);
                }
                catch (ApiException ex)
                {
                    return Json(new KeyValueResult(false, ex.Message), JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new KeyValueResult(false, "Your session has expired. Please sign in again."), JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        public ActionResult GetCallLog(string id)
        {
            //try
            //{
            //    var appointment = _appointmentService.GetById(BaseApiHeader, id);
            //    if (appointment.CallLog != null)
            //    {
            //        foreach (var call in appointment.CallLog)
            //        {

            //            call.StrStatus = ((CallStatus)call.Status).DescriptionAttr();
            //            var sDate = Utils.UnixTimeStampToDateTime(Int64.Parse(call.StartDate),
            //                BaseApiHeader.TimeOffset);
            //            call.StrStartDate = sDate.ToString(GlobalConstant.DateTimeFormat);
            //        }
            //    }
            //    return Json(new KeyValueResult(true, appointment.CallLog), JsonRequestBehavior.AllowGet);
            //}
            //catch (ApiException e)
            //{
            //    return Json(new KeyValueResult(false, e.Message), JsonRequestBehavior.AllowGet);
            //}
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}