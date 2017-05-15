using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using WaxWelio.Abstractions;
using WaxWelio.Common.Config;
using WaxWelio.Common.Enum;
using WaxWelio.Entities.Models;
using WaxWelio.Entities;
using WaxWelio.Common.Exception;

namespace WaxWelio.Web.Controllers
{
    //[AuthorCustom(new UserRole[] {UserRole.RoleHospitalAdmin, UserRole.RoleAdmin})]
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class PricingController : BaseController
    {
        private readonly IPriceService _priceService;

        public PricingController(IPriceService iPriceService)
        {
            _priceService = iPriceService;
        }

        // GET: Pricing
        public ActionResult Index()
        {
            try
            {
                if (Session["auth_info"] != null)
                {
                    var authInfo = (AuthInfo)Session["auth_info"];
                    var data = _priceService.Get(authInfo.CurrentSelectedClinic.ClinicId);
                    ViewBag.idSelected = "pricing";
                    return View(data);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        public ActionResult Edit()
        {
            if (Session["auth_info"] != null)
            {
                var authInfo = (AuthInfo)Session["auth_info"];
                var data = _priceService.Get(authInfo.CurrentSelectedClinic.ClinicId);
                ViewBag.idSelected = "pricing";
                ViewBag.hospitalId = authInfo.CurrentSelectedClinic.ClinicId;
                ViewBag.welioFee = GlobalConstant.WelioFee;
                return View(data);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult AddPrice(string json)
        {
            if (Session["auth_info"] != null)
            {
                try
                {
                    var authInfo = (AuthInfo)Session["auth_info"];
                    IList<PriceModel> listPrice;
                    listPrice = JsonConvert.DeserializeObject<IList<PriceModel>>(json);
                    //foreach (var item in listPrice)
                    //    if (string.IsNullOrEmpty(item.Id))
                    //        listPrice.Remove(item);

                    var model = new EditPriceModel();
                    model.HospitalId = authInfo.CurrentSelectedClinic.ClinicId;
                    model.Prices = listPrice;

                    _priceService.AddPrice(null, model);
                    TempData[GlobalConstant.ErrorTemp] = "List prices successfully updated.";
                    return Json(new {success = "true"}, JsonRequestBehavior.AllowGet);
                }
                catch (ApiException ex)
                {
                    TempData[GlobalConstant.ErrorTemp] = ex.Message;
                    return Json(new {success = "false"}, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                TempData[GlobalConstant.ErrorTemp] = GlobalConstant.SessionExpiredOrNotLogin;
                return RedirectToAction("Index", "Home");
            }
        }
    }
}