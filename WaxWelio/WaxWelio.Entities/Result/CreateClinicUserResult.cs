using Newtonsoft.Json;

namespace WaxWelio.Entities.Result
{
    public class CreateClinicUserResult
    {
        [JsonProperty("DoctorId")]
        public string DoctorId { get; set; }

        [JsonProperty("DoctorAvatar")]
        public string DoctorAvatar { get; set; }

        [JsonProperty("Title")]
        public string TitleUser { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("Phone")]
        public string PhoneNumber { get; set; }

        [JsonProperty("AccessType")]
        public int AccessType { get; set; }

        [JsonProperty("UserType")]
        public int UserType { get; set; }
    }
}
