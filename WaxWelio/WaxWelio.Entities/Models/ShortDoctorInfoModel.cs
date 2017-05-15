using Newtonsoft.Json;

namespace WaxWelio.Entities.Models
{
    public class ShortDoctorInfoModel
    {
        [JsonProperty("ClinicUserId")]
        public string ClinicUserId { get; set; }

        [JsonProperty("ClinicUserActived")]
        public int ClinicUserActived { get; set; }
       
        [JsonProperty("UserType")]
        public int UserType { get; set; }

        [JsonProperty("Owner")]
        public int Owner { get; set; }

        [JsonProperty("AccessType")]
        public int AccessType { get; set; }

        public string StrUserType { get; set; }

        public string StrAccessType { get; set; }

        public string StrClinicUserActived { get; set; }
    }
}
