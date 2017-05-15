using Newtonsoft.Json;

namespace WaxWelio.Entities.Models
{
    public class PatientCreateAppointmentModel
    {
        [JsonProperty("PatientId")]
        public string PatientId { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }
    }
}
