using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WaxWelio.Entities.Models
{
    public class LoginModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [JsonProperty("id")]
        public string Email { get; set; }

        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("timezone")]
        public string TimeZone { get; set; }

        [JsonProperty("appType")]
        public string AppType { get; set; }

        [JsonProperty("lang")]
        public string Language { get; set; }

        [JsonProperty("userTypes")]
        public List<int> UserTypes { get; set; }
    }
}