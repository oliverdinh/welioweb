using System;
using System.Web.Mvc;
using WaxWelio.Abstractions;
using WaxWelio.Common.Config;
using WaxWelio.Common.Enum;
using WaxWelio.Common.Exception;
using WaxWelio.Entities;
using WaxWelio.Entities.Models;

namespace WaxWelio.Web.Controllers
{
    //[AuthorCustom(new UserRole[] { UserRole.RoleHospitalAdmin, UserRole.RoleAdmin })]
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class PaymentController : BaseController
    {
        private readonly IPriceService _priceService;

        public PaymentController(IPriceService priceService)
        {
            _priceService = priceService;
        }

        // GET: Payment
        public ActionResult Index()
        {
            if (Session["auth_info"] != null)
            {
                ViewBag.idSelected = "payments";
                try
                {
                    var authInfo = (AuthInfo)Session["auth_info"];
                    var data = _priceService.GetBankAccount(authInfo.CurrentSelectedClinic.ClinicId);
                    return View(data);
                }
                catch (ApiException ex)
                {
                    TempData[GlobalConstant.ErrorTemp] = ex.Message;
                    return View();
                }
            }
            else
            {
                TempData[GlobalConstant.ErrorTemp] = GlobalConstant.SessionExpiredOrNotLogin;
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Edit()
        {
            if (Session["auth_info"] != null)
            {
                ViewBag.idSelected = "payments";
                try
                {
                    var authInfo = (AuthInfo)Session["auth_info"];
                    var data = _priceService.GetBankAccount(authInfo.CurrentSelectedClinic.ClinicId);
                    return View(data);
                }
                catch (Exception ex)
                {
                    TempData[GlobalConstant.ErrorTemp] = ex.Message;
                    return View(new BankModel());
                }
            }
            else
            {
                TempData[GlobalConstant.ErrorTemp] = GlobalConstant.SessionExpiredOrNotLogin;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult Edit(BankModel model)
        {
            if (Session["auth_info"] != null)
            {
                ViewBag.idSelected = "payments";
                try
                {
                    var authInfo = (AuthInfo)Session["auth_info"];
                    model.ClinicId = authInfo.CurrentSelectedClinic.ClinicId;
                    _priceService.AddOrEditBank(model);
                    TempData[GlobalConstant.ErrorTemp] = "Bank account successfully updated.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData[GlobalConstant.ErrorTemp] = ex.Message;
                    return View(new BankModel());
                }
            }
            else
            {
                TempData[GlobalConstant.ErrorTemp] = GlobalConstant.SessionExpiredOrNotLogin;
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Export()
        {
            ViewBag.idSelected = "reports";
            return View();
        }
    }
}