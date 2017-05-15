using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WaxWelio.Entities.Models
{
    public class SignUpModel : BaseModel
    {
        [Required]
        [RegularExpression(@".*[^ ].*", ErrorMessage = "Not allow only spaces")]
        [JsonProperty(PropertyName = "ClinicName")]
        [DisplayName("Clinic name")]
        public string ClinicName { get; set; }

        [JsonProperty(PropertyName = "ClinicEmail")]
        public string ClinicEmail { get; set; }

        [Required]
        [RegularExpression(@".*[^ ].*", ErrorMessage = "Not allow only spaces")]
        [JsonProperty(PropertyName = "AddressLine1")]
        [DisplayName("Clinic address - Line 1")]
        public string AddressLine1 { get; set; }

        [JsonProperty(PropertyName = "AddressLine2")]
        public string AddressLine2 { get; set; }

        [Required]
        [RegularExpression(@".*[^ ].*", ErrorMessage = "Not allow only spaces")]
        [JsonProperty(PropertyName = "State")]
        [DisplayName("Suburb")]
        public string State { get; set; }

        [Required]
        [RegularExpression(@".*[^ ].*", ErrorMessage = "Not allow only spaces")]
        [JsonProperty(PropertyName = "PostCode")]
        [DisplayName("Postcode")]
        public string PostCode { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[0-9])[- +()0-9]+$", ErrorMessage = "Invalid phone number")]
        [JsonProperty(PropertyName = "Phone")]
        [DisplayName("Phone number")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "DoctorTitle")]
        public string DoctorTitle { get; set; }

        [Required]
        [RegularExpression(@".*[^ ].*", ErrorMessage = "Not allow only spaces")]
        [JsonProperty(PropertyName = "DoctorFirstName")]
        [DisplayName("First name")]
        public string DoctorFirstName { get; set; }

        [Required]
        [RegularExpression(@".*[^ ].*", ErrorMessage = "Not allow only spaces")]
        [JsonProperty(PropertyName = "DoctorLastName")]
        
        [DisplayName("Last name")]
        public string DoctorLastName { get; set; }

        [Required]
        [RegularExpression(@".*[^ ].*", ErrorMessage = "Not allow only spaces")]
        [EmailAddress]
        [JsonProperty(PropertyName = "DoctorEmail")]
        
        [DisplayName("Email")]
        public string DoctorEmail { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[0-9])[- +()0-9]+$", ErrorMessage = "Invalid phone number")]
        [JsonProperty(PropertyName = "DoctorPhoneNumber")]
        [DisplayName("Phone number")]
        public string DoctorPhoneNumber { get; set; }
    }
}
