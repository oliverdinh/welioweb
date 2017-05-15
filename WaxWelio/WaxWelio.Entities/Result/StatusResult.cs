using Newtonsoft.Json;

namespace WaxWelio.Entities.Result
{
    public class StatusResult
    {
        [JsonProperty("Status")]
        public bool Status { get; set; }

        [JsonProperty("Total")]
        public int Total { get; set; }
    }
}
