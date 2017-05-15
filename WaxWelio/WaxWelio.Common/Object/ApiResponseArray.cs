using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WaxWelio.Common.Object
{
    public class ApiResponseArray
    {
        [JsonProperty(PropertyName = "result")]
        public JObject[] Result { get; set; }

        [JsonProperty(PropertyName = "successed")]
        public bool Successed { get; set; }

        [JsonProperty(PropertyName = "errorCode")]
        public int ErrorCode { get; set; }

        [JsonProperty(PropertyName = "ErrorMessage")]
        public string ErrorMessage { get; set; }
    }
}