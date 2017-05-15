using Newtonsoft.Json;

namespace WaxWelio.Entities.Result
{
    public class Photos
    {
        [JsonProperty("logo")]
        public string Logo { get; set; }
    }
}