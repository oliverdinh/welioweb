using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using WaxWelio.Abstractions;
using WaxWelio.Common.Config;
using WaxWelio.Common.Enum;
using WaxWelio.Common.Exception;
using WaxWelio.Common.Object;
using WaxWelio.Entities.Models;

namespace WaxWelio.Web.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class PatientController : BaseController
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        // GET: Patient
        public ActionResult Index()
        {
            ViewBag.IdSelected = "patientUsers";
            return View();
        }

        public ActionResult ListPatients(JQueryDataTableParamModel param)
        {
            try
            {
                var patients = _patientService.ListPatients(param.iDisplayStart, param.iDisplayLength, param.sSearch);
                return Json(new
                {
                    recordsTotal = patients.Count,
                    recordsFiltered = _patientService.Total(),
                    data = patients
                }, JsonRequestBehavior.AllowGet);
            }
            catch (ApiException)
            {
                return Json(new
                {
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    data = new List<PatientModel>()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult PatientDetails(string id)
        {
            ViewBag.IdSelected = "patientUsers";
            try
            {
                var patient = _patientService.GetById(null, id);
                return View(patient);
            }
            catch (ApiException ex)
            {
                TempData[GlobalConstant.ErrorTemp] = ex.Message;
                return RedirectToAction("PatientDetails", "Patient", new { id });
            }
        }

        public ActionResult EditDetails(string id)
        {
            try
            {
                ViewBag.idSelected = "patientUsers";
                var patient = _patientService.GetById(null, id);
                return View(patient);
            }
            catch (ApiException ex)
            {
                TempData[GlobalConstant.ErrorTemp] = ex.Message;
                return RedirectToAction("EditDetails", "Patient", new { id });
            }
        }
        [HttpPost]
        public ActionResult EditDetails(PatientModel model)
        {
            try
            {
                var dataurl = Request["image-data"];
                dataurl = dataurl.Replace("data:image/png;base64,", "");
                byte[] data = Convert.FromBase64String(dataurl);
                var fileName = Request["image-name"];
                var input = new UpdatePatientModel
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PatientId = model.PatientId,
                    Phone = model.Phone,
                };
                if (data.Length > 0)
                {
                    input.FileName = fileName;
                    input.Image = data;
                }
                _patientService.UpdatePatient(input);
                return RedirectToAction("PatientDetails", new { model.PatientId });
            }
            catch (Exception)
            {
                return View(model);
            }
        }
    }
}