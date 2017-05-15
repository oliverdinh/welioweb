using Newtonsoft.Json;

namespace WaxWelio.Common.Object
{
    public class ApiPassword365Profile
    {
        [JsonProperty("forceChangePasswordNextSignIn")]
        public bool ForceChangePasswordNextSignIn { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
