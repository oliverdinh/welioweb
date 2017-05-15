using Newtonsoft.Json;

namespace WaxWelio.Common.Object
{
    public class Api365Error
    {
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}
