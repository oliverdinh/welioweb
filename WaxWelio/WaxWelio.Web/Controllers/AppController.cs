using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WaxWelio.Abstractions;
using WaxWelio.Common.Config;
using WaxWelio.Common.Exception;
using WaxWelio.Entities.Models;

namespace WaxWelio.Web.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class AppController : BaseController
    {
        private readonly IUserService _userService;

        public AppController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: App
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string email, string hash)
        {
            RemoveCookie();
            return RedirectToAction("ResetPasswords", new { email, hash });
        }

        [AllowAnonymous]
        public ActionResult ResetPasswords(string email, string hash)
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
            catch (ApiException ex)
            {
                if (ex.ErrorCode.Equals("133"))
                {
                    return RedirectToAction("LinkUsed");
                }
                else
                {
                    return RedirectToAction("LinkExpired");
                }

            }
        }

        [AllowAnonymous]
        public ActionResult RemoveCookieRedirect()
        {
            RemoveCookie();
            return RedirectToAction("ResetPasswordComplete");
        }

        [AllowAnonymous]
        public ActionResult ResetPasswordComplete()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult LinkUsed()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult LinkExpired()
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
    }
}