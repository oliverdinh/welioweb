using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WaxWelio.Entities.Models
{
    public class NotifyClinicClinicModel
    {
        [DisplayName("Clinic Name")]
        [Required]
        [JsonProperty("name")]
        public string ClinicName { get; set; }

        [Required]
        [JsonProperty("suburb")]
        public string Suburd { get; set; }

        [DisplayName("State")]
        [Required]
        [JsonProperty("postCode")]
        public string PostCode { get; set; }
    }
}
