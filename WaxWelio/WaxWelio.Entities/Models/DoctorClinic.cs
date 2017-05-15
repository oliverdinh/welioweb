using Newtonsoft.Json;
using WaxWelio.Entities.Result;

namespace WaxWelio.Entities.Models
{
    public class DoctorClinic
    {
        [JsonProperty(PropertyName = "ClinicUserActived")]
        public int ClinicUserActived { get; set; }

        [JsonProperty(PropertyName = "ClinicUserUserType")]
        public int ClinicUserUserType { get; set; }

        [JsonProperty(PropertyName = "ClinicUserOwner")]
        public int ClinicUserOwner { get; set; }

        [JsonProperty(PropertyName = "ClinicUserAccessType")]
        public int ClinicUserAccessType { get; set; }

        [JsonProperty(PropertyName = "Clinic")]
        public ClinicResult Clinic { get; set; }
    }
}
