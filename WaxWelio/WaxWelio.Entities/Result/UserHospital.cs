using System.Collections.Generic;
using Newtonsoft.Json;

namespace WaxWelio.Entities.Result
{
    public class UserHospital
    {
        [JsonProperty("hospitalId")]
        public string HospitalId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("photos")]
        public Photos Photos { get; set; }

        [JsonProperty("roles")]
        public List<int> Roles { get; set; }

        [JsonProperty("subType")]
        public int SubType { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }
    }
}
