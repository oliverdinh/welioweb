using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaxWelio.Common.Object
{
    public class Api365UserInfoResponse
    {
        [JsonProperty(PropertyName = "mail")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "mobilePhone")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "error")]
        public Api365Error Error { get; set; }
    }
}
