using System;
using System.Web.Mvc;
using OfficeOpenXml;
using WaxWelio.Abstractions;
using WaxWelio.Common;
using WaxWelio.Common.Config;
using WaxWelio.Common.Enum;
using WaxWelio.Common.Exception;

namespace WaxWelio.Web.Controllers
{
    //[AuthorCustom(new UserRole[] { UserRole.RoleHospitalAdmin, UserRole.RoleAdmin })]
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class ReportController : BaseController
    {
        private readonly IAppointmentService _appointmentService;

        public ReportController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public ActionResult Index()
        {
            ViewBag.idSelected = "reports";
            return View();
        }

        public ActionResult Export(string startDate, string endDate)
        {
            try
            {
                var startDateSelectStr = string.IsNullOrEmpty(startDate)
                    ? DateTime.Today
                    : Utils.StringToDateTime(startDate, GlobalConstant.DateFormat);
                var startDateSelect = Utils.ConvertToTimestamp(startDateSelectStr);
                var endDateSelectStr = string.IsNullOrEmpty(endDate)
                    ? DateTime.Today : Utils.StringToDateTime(endDate, GlobalConstant.DateFormat);
                var endDateSelect = Utils.ConvertToTimestamp(endDateSelectStr.AddDays(1));
                var appointments = _appointmentService.Get(BaseApiHeader, BaseApiHeader.HospitalSelected, string.Empty,
                    string.Empty, startDateSelect, endDateSelect,
                    GlobalConstant.StartIndex,
                    GlobalConstant.Length, string.Empty, false,
                    SortField.ExpectedStartDate,
                    SortType.Asc);
                using (var excelPackage = new ExcelPackage())
                {
                    excelPackage.Workbook.Properties.Author = "Welio";
                    excelPackage.Workbook.Properties.Title = "Appointment Export";
                    var sheet = excelPackage.Workbook.Worksheets.Add("Appointment Report");
                    var row1 = 1;
                    var rowIndex = 2;


                    sheet.Cells[row1, 1].Value = "Date";
                    sheet.Cells[row1, 2].Value = "Doctor";
                    sheet.Cells[row1, 3].Value = "Patient";
                    sheet.Cells[row1, 4].Value = "Carer";
                    sheet.Cells[row1, 5].Value = "Phone number";
                    sheet.Cells[row1, 6].Value = "Status";
                    sheet.Cells[row1, 7].Value = "Start";
                    sheet.Cells[row1, 8].Value = "Actual Start";
                    sheet.Cells[row1, 9].Value = "Duration";
                    sheet.Cells[row1, 10].Value = "Actual Duration";
                    sheet.Cells[row1, 11].Value = "Fee";
                    sheet.Cells[row1, 12].Value = "Actual Fee";

                    for (var i = 1; i < 13; i++)
                    {
                        sheet.Cells[row1, i].Style.Font.Bold = true;
                        sheet.Cells[row1, i].Style.Font.Size = 12;
                        sheet.Cells[row1, i].AutoFitColumns();
                    }


                    foreach (var item in appointments)
                    {
                        var col = 1;
                        var actualDateTime = item.ActualStartDateTime == null
                            ? (DateTime?)null
                            : Utils.UnixTimeStampToDateTime(item.ActualStartDateTime.Value);
                        var dateTime = Utils.UnixTimeStampToDateTime(item.ActualStartDateTime.Value);

                        var resultDuration = TimeSpan.FromMinutes(item.ExpectedDuration);
                        var duration = resultDuration.ToString(@"hh\:mm\:ss");
                        var resultActualDuration = new TimeSpan();
                        var actualDuration = item.ActualDuration;
                        if (actualDuration != null)
                            resultActualDuration = TimeSpan.FromSeconds((int)actualDuration);

                        sheet.Cells[rowIndex, col++].Value = dateTime.ToString(GlobalConstant.DateFormat);
                        sheet.Cells[rowIndex, col++].Value = item.Doctor.FullName;
                        sheet.Cells[rowIndex, col++].Value = item.Patient.FullName;
                        sheet.Cells[rowIndex, col++].Value = item.PatientFullName;
                        sheet.Cells[rowIndex, col++].Value = item.Patient.Phone;
                        sheet.Cells[rowIndex, col++].Value = ((AppointmentStatus)item.Status).DescriptionAttr();
                        sheet.Cells[rowIndex, col++].Value = dateTime.ToString(GlobalConstant.TimeFormat);
                        sheet.Cells[rowIndex, col++].Value = actualDateTime?.ToString(GlobalConstant.TimeFormat) ?? "";
                        sheet.Cells[rowIndex, col++].Value = duration;
                        sheet.Cells[rowIndex, col++].Value = resultActualDuration.TotalMinutes > 0
                            ? resultActualDuration.ToString(@"hh\:mm\:ss")
                            : "";
                        sheet.Cells[rowIndex, col++].Value = "$ " + item.ExpectedFee;

                        sheet.Cells[rowIndex, col].Value = item.ActualFee == 0 ? "" : "$" + item.ActualFee;
                        for (var i = 1; i < 13; i++)
                            if (sheet.Cells[rowIndex, i].Text.Length > sheet.Cells[rowIndex - 1, i].Text.Length)
                                sheet.Cells[rowIndex, i].AutoFitColumns();
                        rowIndex++;
                    }

                    Response.ClearContent();
                    Response.BinaryWrite(excelPackage.GetAsByteArray());
                    Response.AddHeader("Content-Disposition", "attachment; filename=AppointmentRecordExport_V1.csv");
                    Response.ContentType = "text/csv";
                    Response.Flush();
                    Response.End();
                }
            }
            catch (ApiException)
            {
                return View("Index");
            }
            return View("Index");
        }
    }
}