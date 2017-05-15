using System.Security.AccessControl;
using Newtonsoft.Json;

namespace WaxWelio.Entities.Models
{
    public class CreateUpdateAppointmentModel : BaseModel
    {
        [JsonProperty("IsFloating")]
        public bool IsFloating { get; set; }

        [JsonProperty("AppointmentId")]
        public string AppointmentId { get; set; }

        [JsonProperty("IsNewPatient")]
        public bool IsNewPatient { get; set; }

        [JsonProperty("IsCarer")]
        public int IsCarer { get; set; }

        [JsonProperty("PatientId")]
        public string PatientId { get; set; }

        [JsonProperty("ClinicId")]
        public string ClinicId { get; set; }

        [JsonProperty("DoctorId")]
        public string DoctorId { get; set; }

        [JsonProperty("DoctorName")]
        public string DoctorName { get; set; }

        [JsonProperty("ClinicName")]
        public string ClinicName { get; set; }

        [JsonProperty("ClinicPhone")]
        public string ClinicPhone { get; set; }

        [JsonProperty("ExpectedStartDateTime")]
        public long ExpectedStartDateTime { get; set; }

        [JsonProperty("ExpectedDuration")]
        public int ExpectedDuration { get; set; }

        [JsonProperty("ExpectedFee")]
        public string ExpectedFee { get; set; }

        [JsonProperty("CarerFirstName")]
        public string CarerFirstName { get; set; }

        [JsonProperty("CarerLastName")]
        public string CarerLastName { get; set; }

        [JsonProperty("MeetingUri")]
        public string MeetingUri { get; set; }

        [JsonProperty("JoinMettingUrl")]
        public string JoinMettingUrl { get; set; }  

        [JsonProperty("Patient")]
        public PatientCreateAppointmentModel Patient { get; set; }

    }
}