using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;
using WaxWelio.Common.Config;
using WaxWelio.Entities;
using WaxWelio.Entities.Data;
using WaxWelio.Web.HelperClasses;

namespace WaxWelio.Web.Controllers
{
    public class HealthCareController : Controller
    {
        // GET: HealthCare
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Conference(string meetingUrl, string meetingUri, string appointmentId, string patientName)
        {
            var authInfo = (AuthInfo)Session["auth_info"];
            if (authInfo == null)
            {
                TempData[GlobalConstant.ErrorTemp] = GlobalConstant.SessionExpired;
                return View();
            }
            if (!string.IsNullOrEmpty(meetingUrl) && !string.IsNullOrEmpty(meetingUri))
            {
                Session["meeting_info"] = new MeetingInfo
                {
                    MeetingUrl = meetingUrl,
                    MeetingUri = meetingUri,
                    AppointmentId = appointmentId,
                    PatientName = patientName
                };
            };
            return View();
        }

        public ActionResult ConfigureDevices(string from)
        {
            return View();
        }

        [HttpPost]
        public async Task<string> GetAnonMeeting(string subject, string description)
        {
            return await Helper.GetAnonMeeting(subject, description);
        }

        [HttpPost]
        public async Task<string> GetAnonToken(string applicationSessionId, string allowedOrigins, string meetingUrl)
        {
            var jsonResponseString = await GetAnonTokenJob(applicationSessionId, allowedOrigins, meetingUrl);
            //// TODO: Move this method to service class
            return JsonConvert.DeserializeObject(jsonResponseString).ToString();
        }

        private async Task<string> GetAnonTokenJob(string applicationSessionId, string allowedOrigins, string meetingUrl)
        {
            string jsonResponseString;
            try
            {
                var jsonobject = new JObject
                {
                    {"ApplicationSessionId", applicationSessionId},
                    {"AllowedOrigins", allowedOrigins},
                    {"MeetingUrl", meetingUrl}
                };

                using (var wc = new WebClient())
                {
                    string trustedApiUri = $"{WebConfigurationManager.AppSettings["TrustedApi"]}/GetAnonTokenJob";
                    wc.Headers.Add(HttpRequestHeader.Accept, "application/json");
                    wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    wc.Headers.Add("user-agent", "Other");
                    jsonResponseString = await wc.UploadStringTaskAsync(new Uri(trustedApiUri), "POST", jsonobject.ToString());
                }
            }
            catch
            {
                throw;
            }

            return jsonResponseString;
        }
    }
}