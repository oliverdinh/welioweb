using System;
using System.Web.Mvc;
using WaxWelio.Abstractions;
using WaxWelio.Common.Config;
using WaxWelio.Common.Exception;
using WaxWelio.Common.Object;
using WaxWelio.Entities;
using WaxWelio.Services;

namespace WaxWelio.Web.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class HomeController : BaseController
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            var authInfo = Session["auth_info"] == null ? null : (AuthInfo)Session["auth_info"];
            if(authInfo != null)
            {
                if(authInfo.DoctorClinics.Length == 1)
                {
                    authInfo.CurrentSelectedClinic = authInfo.DoctorClinics[0].Clinic;
                }
            }

            if (authInfo != null)
            {
                var isAdmin = ParseBool(authInfo.Admin.ToString());
                if (isAdmin)
                {
                    ViewBag.Title = "Active Clinics";
                    return RedirectToAction("ActiveClinics", "Clinic");
                }
                else if(authInfo.CurrentSelectedClinic != null)
                {
                    ViewBag.Title = "Appointment";
                    return RedirectToAction("Index", "Appointment");
                } else return View();
            }
            else return View();
        }

        public ActionResult LocalStorage()
        {
            return View();
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

        public ActionResult SaveToken(string token)
        {
            try
            {
                string _365email = _userService.getEmail365(token);
                var authInfo = _userService.GetAuthInfo(_365email);
                Session["token"] = token;
                Session["auth_info"] = authInfo;
                return Json(new KeyValueResult(true), JsonRequestBehavior.AllowGet);
            }
            catch (ApiException ex)
            {
                return Json(new KeyValueResult(false, ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
    }
}