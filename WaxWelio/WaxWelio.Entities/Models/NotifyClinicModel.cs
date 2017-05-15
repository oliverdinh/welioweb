using Newtonsoft.Json;

namespace WaxWelio.Entities.Models
{
   public class NotifyClinicModel
    {
        [JsonProperty("DoctorName")]
        public string DoctorName { get; set; }

        [JsonProperty("ClinicName")]
        public string ClinicName { get; set; }

        [JsonProperty("Suburb")]
        public string Suburb { get; set; }

        [JsonProperty("State")]
        public string State { get; set; }
    }
}
