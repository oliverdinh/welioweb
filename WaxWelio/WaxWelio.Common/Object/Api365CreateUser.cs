using Newtonsoft.Json;

namespace WaxWelio.Common.Object
{
    public class Api365CreateUser
    {
        [JsonProperty("accountEnabled")]
        public bool AccountEnabled { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("mailNickname")]
        public string MailNickname { get; set; }

        [JsonProperty("userPrincipalName")]
        public string UserPrincipalName { get; set; }

        [JsonProperty("passwordProfile")]
        public ApiPassword365Profile PasswordProfile { get; set; }
    }
}
