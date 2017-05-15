using Newtonsoft.Json;

namespace WaxWelio.Entities.Models
{
    public class CancelAppoinmentModel
    {
        [JsonProperty("AppointmentId")]
        public string AppointmentId { get; set; }

        [JsonProperty("DoctorName")]
        public string DoctorName { get; set; }

        [JsonProperty("ClinicName")]
        public string ClinicName { get; set; }

        [JsonProperty("ClinicPhone")]
        public string ClinicPhone { get; set; }

        [JsonProperty("ExpectedStartDateTime")]
        public long? ExpectedStartDateTime { get; set; }

        [JsonProperty("Patient")]
        public PatientCreateAppointmentModel Patient { get; set; }
    }
}
