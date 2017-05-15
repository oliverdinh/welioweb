using Newtonsoft.Json;

namespace WaxWelio.Entities.Models
{
    public class PriceModel:BaseModel
    {
        [JsonProperty("PricingId")]
        public string PricingId { get; set; }

        [JsonProperty("Duration")]
        public int Duration { get; set; }

        [JsonProperty("PatientFee")]
        public int PatientFee { get; set; }

        [JsonProperty("WelioFee")]
        public int WelioFee { get; set; }

        [JsonProperty("ClinicId")]
        public string ClinicId { get; set; }

        [JsonProperty("Status")]
        public int Status { get; set; }
    }
}