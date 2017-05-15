using System.Collections.Generic;
using Newtonsoft.Json;

namespace WaxWelio.Entities.Models
{
    public class EditPriceModel:BaseModel
    {
        [JsonProperty("ClinicId")]
        public string HospitalId { get; set; }

        [JsonProperty("Pricing")]
        public IList<PriceModel> Prices { get; set; }
    }
}
