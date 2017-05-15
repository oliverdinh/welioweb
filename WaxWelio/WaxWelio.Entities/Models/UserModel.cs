using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WaxWelio.Entities.Models
{
    public class UserModel : BaseModel
    {        
        [JsonProperty("DoctorId")]
        public string DoctorId { get; set; }

        [JsonProperty("DoctorAvatar")]
        public string DoctorAvatar { get; set; }

        [JsonProperty("Title")]
        public string TitleUser { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [Display(Name = "First name")]
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        [Required(ErrorMessage = "Last Name is required.")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [Display(Name = "Email")]
        [JsonProperty("email")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Email Office 365")]
        [JsonProperty("Office365")]
        [EmailAddress]
        public string Office365 { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Display(Name = "Phone number")]
        [RegularExpression(@"^((\+[1-9]{1,4}[ \-]*)|(\([0-9]{2,3}\)[ \-]*)|([0-9]{2,4})[ \-]*)*?[0-9]{3,4}?[ \-]*[0-9]{3,4}?$", ErrorMessage = "Phone number is invalid.")]
        [JsonProperty("Phone")]
        public string PhoneNumber { get; set; }

        public string FullName => FirstName + " " + LastName;

        [JsonProperty("Admin")]
        public int Admin { get; set; }

        [JsonProperty("Actived")]
        public int Actived { get; set; }

        public string Status { get; set; }

        [JsonProperty("Office365TempPassword")]
        public string Office365TempPassword { get; set; }

        [JsonProperty("ClinicUsers")]
        public DoctorInfoModel[] ClinicUsers { get; set; }

        //public string StrStatus { get; set; }

        //[JsonProperty("hash")]
        //public string Hash { get; set; }

        //[JsonProperty("login")]
        //public bool Login { get; set; }

        //[JsonProperty("photo")]
        //public string Photo { get; set; }

        //public string FilePath { get; set; }

        public int AccessType { get; set; }

        public int UserType { get; set; }

        public string FileName { get; set; }

        public byte[] Imagefile { get; set; }

        public string AppointmentCount { get; set; }
    }
}