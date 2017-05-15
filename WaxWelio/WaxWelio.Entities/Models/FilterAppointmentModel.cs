using Newtonsoft.Json;

namespace WaxWelio.Entities.Models
{
    public class FilterAppointmentModel
    {
        [JsonProperty(PropertyName = "SelectDate")]
        public string SelectDate { get; set; }

        [JsonProperty(PropertyName = "Type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "DoctorId")]
        public string DoctorId { get; set; }

        [JsonProperty(PropertyName = "ClinicId")]
        public string ClinicId { get; set; }

        [JsonProperty(PropertyName = "Start")]
        public int Start { get; set; }

        [JsonProperty(PropertyName = "Limit")]
        public int Limit { get; set; }

        [JsonProperty(PropertyName = "FromTime")]
        public long FromTime { get; set; }

        [JsonProperty(PropertyName = "ToTime")]
        public long ToTime { get; set; }

        [JsonProperty(PropertyName = "OrderByStartDateTime")]
        public string OrderByStartDateTime { get; set; }

        [JsonProperty(PropertyName = "Keywords")]
        public string Keywords { get; set; }
    }
}
