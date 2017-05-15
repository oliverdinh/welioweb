using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using WaxWelio.Entities.Models;

namespace WaxWelio.Entities.Result
{
    public class ClinicResult : BaseModel
    {
        [JsonProperty(PropertyName = "ClinicId")]
        public string ClinicId { get; set; }

        [Display(Name = "Address Line 1")]
        [Required]
        [JsonProperty(PropertyName = "Street1")]
        public string Street1 { get; set; }

        [Display(Name = "Address Line 2")]
        [JsonProperty(PropertyName = "Street2")]
        public string Street2 { get; set; }

        [Display(Name = "State")]
        [Required]
        [JsonProperty(PropertyName = "State")]
        public string State { get; set; }

        [Display(Name = "PostCode")]
        [Required]
        [JsonProperty(PropertyName = "PostCode")]
        public string PostCode { get; set; }

        [Display(Name = "Phone")]
        [Required]
        [JsonProperty(PropertyName = "Phone")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "Actived")]
        public int Actived { get; set; }

        [Display(Name = "Clinic Name")]
        [Required]
        [JsonProperty(PropertyName = "ClinicName")]
        public string ClinicName { get; set; }

        [Display(Name = "Clinic Email")]
        [JsonProperty(PropertyName = "ClinicEmail")]
        public string ClinicEmail { get; set; }

        [JsonProperty(PropertyName = "Created")]
        public string Created { get; set; }

        [JsonProperty(PropertyName = "Updated")]
        public string Updated { get; set; }

        public string StatusName { get; set; }
    }
}
