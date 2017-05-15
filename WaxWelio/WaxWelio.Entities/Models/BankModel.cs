using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WaxWelio.Entities.Models
{
    public class BankModel : BaseModel
    {
        [JsonProperty("ClinicId")]
        public string ClinicId { get; set; }

        [Required]
        [DisplayName("Account Name")]
        [JsonProperty("AccountName")]
        public string AccountName { get; set; }

        [Required]
        [JsonProperty("BankName")]
        public string BSB { get; set; }

        [Required]
        [DisplayName("Account Number")]
        [JsonProperty("AccountNumber")]
        public string AccountNumber { get; set; }
    }
}
