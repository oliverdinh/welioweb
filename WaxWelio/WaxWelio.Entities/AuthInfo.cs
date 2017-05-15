using Newtonsoft.Json;
using WaxWelio.Entities.Models;
using WaxWelio.Entities.Result;

namespace WaxWelio.Entities
{
    public class AuthInfo
    {
        [JsonProperty(PropertyName = "DoctorId")]
        public string DoctorId { get; set; }

        [JsonProperty(PropertyName = "DoctorAvatar")]
        public string DoctorAvatar { get; set; }

        [JsonProperty(PropertyName = "Title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "FirstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "LastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "Phone")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "Office365")]
        public string Office365 { get; set; }

        [JsonProperty(PropertyName = "Actived")]
        public int Actived { get; set; }

        [JsonProperty(PropertyName = "Admin")]
        public int Admin { get; set; }

        [JsonProperty(PropertyName = "DoctorClinics")]
        public DoctorClinic[] DoctorClinics { get; set; }

        public string FullName => FirstName + " " + LastName;

        public ClinicResult CurrentSelectedClinic { get; set; }

        public bool IsAdminBool => Admin == 1;
    }
}
