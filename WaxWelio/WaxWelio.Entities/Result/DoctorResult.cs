using Newtonsoft.Json;

namespace WaxWelio.Entities.Result
{
    public class DoctorResult
    {
        [JsonProperty("DoctorId")]
        public string DoctorId { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Office365")]
        public string Office365 { get; set; }

        [JsonProperty("Actived")]
        public int Actived { get; set; }

        [JsonProperty("Admin")]
        public int Admin { get; set; }

        [JsonProperty("Office365TempPassword")]
        public string Office365TempPassword { get; set; }

        public string FullName => FirstName + " " + LastName;
    }
}
