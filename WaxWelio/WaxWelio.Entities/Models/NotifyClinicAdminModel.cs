using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WaxWelio.Entities.Models
{
    public class NotifyClinicAdminModel
    {
        [DisplayName("Doctor's Name")]
        [Required]
        [JsonProperty("firstName")]
        public string AdminName { get; set; }
    }
}
