using System.Collections.Generic;
using Newtonsoft.Json;
using WaxWelio.Entities.Models;
using WaxWelio.Entities.Result;

namespace WaxWelio.Common.Object
{
    public class ApiHeader
    {
        [JsonProperty(PropertyName = "lang")]
        public string Lang { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "sessionId")]
        public string SessionId { get; set; }

        [JsonProperty("sessionExpired")]
        public long SessionExpired { get; set; }

        [JsonProperty("appType")]
        public int AppType { get; set; }

        [JsonProperty("userType")]
        public int UserType { get; set; }

        [JsonProperty("hospitals")]
        public List<UserHospital> Hospitals { get; set; }

        public string HospitalName { get; set; }
        public string HospitalSelected { get; set; }

        public int HospitalSubType { get; set; }

        public List<int> HospitalRoles { get; set; }

        [JsonProperty("profile")]
        public UserModel User { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        public double TimeOffset { get; set; }
    }
}