using Newtonsoft.Json;
namespace WaxWelio.Entities.Models
{
    public class ClinicModel
    {
        [JsonProperty(PropertyName = "ClinicId")]
        public string ClinicId { get; set; }

        [JsonProperty(PropertyName = "Street1")]
        public string Street1 { get; set; }

        [JsonProperty(PropertyName = "Street2")]
        public string Street2 { get; set; }

        [JsonProperty(PropertyName = "State")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "PostCode")]
        public string PostCode { get; set; }

        [JsonProperty(PropertyName = "Phone")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "Status")]
        public int Status { get; set; }

        [JsonProperty(PropertyName = "ClinicName")]
        public string ClinicName { get; set; }

        [JsonProperty(PropertyName = "ClinicEmail")]
        public string ClinicEmail { get; set; }
    }
}
