
using Newtonsoft.Json;

namespace WaxWelio.Entities.Models
{
    public class DoctorInfoModel
    {
        [JsonProperty("ClinicUserId")]
        public string ClinicUserId { get; set; }

        [JsonProperty("UserType")]
        public int UserType { get; set; }

        [JsonProperty("Owner")]
        public int Owner { get; set; }

        [JsonProperty("AccessType")]
        public int AccessType { get; set; }

        [JsonProperty("ClinicUserActived")]
        public int ClinicUserActived { get; set; }

        public string UserTypeStr { get; set; }

        public string AccessTypeStr { get; set; }

        public string ClinicUserActivedStr { get; set; }

        [JsonProperty("Clinic")]
        public ClinicModel Clinic { get; set; }
    }
}
