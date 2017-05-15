using Newtonsoft.Json;

namespace WaxWelio.Entities.Models
{
    public class PatientModel : BaseModel
    {
        [JsonProperty("PatientId")]
        public string PatientId { get; set; }

        [JsonProperty("PatientAvatar")]
        public string PatientAvatar { get; set; }
        
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Actived")]
        public int Actived { get; set; }

        [JsonProperty("IsFoalting")]
        public int IsFoalting { get; set; }

        public string FullName => FirstName + " " + LastName;
    }
}