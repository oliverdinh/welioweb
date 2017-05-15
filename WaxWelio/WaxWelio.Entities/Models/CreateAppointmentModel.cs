using System.Security.AccessControl;
using Newtonsoft.Json;

namespace WaxWelio.Entities.Models
{
    public class CreateAppointmentModel
    {
        [JsonProperty("patientId")]
        public string PatientId { get; set; }

        [JsonProperty("hospitalId")]
        public string HospitalId { get; set; }

        [JsonProperty("doctorId")]
        public string DoctorId { get; set; }

        [JsonProperty("expectedStartDate")]
        public long ExpectedStartDate { get; set; }

        [JsonProperty("expectedDuration")]
        public int ExpectedDuration { get; set; }

        [JsonProperty("otherFirstName")]
        public string OtherFirstName { get; set; }

        [JsonProperty("otherLastName")]
        public string OtherLastName { get; set; }

        [JsonProperty("sipUriMeeting")]
        public string SipUriMeeting { get; set; }

        [JsonProperty("urlJoinMeeting")]
        public string UrlJoinMeeting { get; set; }

        [JsonProperty("patient")]
        public PatientCreateAppointmentModel Patient { get; set; }
        
    }
}