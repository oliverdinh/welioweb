using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WaxWelio.Entities.Models
{
    public class CreateNewPasswordModel
    {
        [JsonProperty(PropertyName = "oldPassword")]
        public string OldPassword { get; set; }

        [JsonProperty(PropertyName = "hash")]
        public string Hash { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "newPassword")]
        [Required]
        [RegularExpression(@"(?=.*[A-Z])(?=.*\W).{6,}", ErrorMessage = "Password must contain at least 6 characters including at least 1 uppercase letter and 1 special character")]
        public string Password { get; set; }

        [DisplayName("Re-type Password")]
        [Compare("Password",ErrorMessage="Password miss - match")]
        public string Re_password { get; set; }

        public string TimeZone { get; set; }
    }
}
