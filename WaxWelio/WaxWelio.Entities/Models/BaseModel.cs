using Newtonsoft.Json;

namespace WaxWelio.Entities.Models
{
    public class BaseModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        public int No { get; set; }
    }
}