using Newtonsoft.Json;

namespace WaxWelio.Entities.Models
{
    public class UpdatePatientModel
    {
        [JsonProperty("PatientId")]
        public string PatientId { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        public string FileName { get; set; }

        public byte[] Image { get; set; }
    }
}
