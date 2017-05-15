using System.Collections.Generic;
using Newtonsoft.Json;
using WaxWelio.Entities.Models;

namespace WaxWelio.Entities.Result
{
    public class AppointmentResult : BaseModel
    {
        [JsonProperty("AppointmentId")]
        public string AppointmentId { get; set; }

        [JsonProperty("PatientLastName")]
        public string PatientLastName { get; set; }

        [JsonProperty("PatientFirstName")]
        public string PatientFirstName { get; set; }

        [JsonProperty("IsCarer")]
        public int IsCarer { get; set; }

        [JsonProperty("ExpectedDuration")]
        public int ExpectedDuration { get; set; }

        [JsonProperty("ExpectedFee")]
        public decimal? ExpectedFee { get; set; }

        [JsonProperty("ExpectedStartDateTime")]
        public long ExpectedStartDateTime { get; set; }

        [JsonProperty("ActualDuration")]
        public int? ActualDuration { get; set; }

        [JsonProperty("ActualStartDateTime")]
        public long? ActualStartDateTime { get; set; }

        [JsonProperty("ActualFee")]
        public decimal? ActualFee { get; set; }

        [JsonProperty("JoinMettingUrl")]
        public string JoinMettingUrl { get; set; }

        [JsonProperty("Status")]
        public int Status { get; set; }

        [JsonProperty("MeetingId")]
        public string MeetingId { get; set; }

        [JsonProperty("MeetingUri")]
        public string MeetingUri { get; set; }

        [JsonProperty("Patient")]
        public PatientResult Patient { get; set; }

        [JsonProperty("Doctor")]
        public DoctorResult Doctor { get; set; }

        public string PatientFullName => PatientFirstName + " " + PatientLastName;
    }
}