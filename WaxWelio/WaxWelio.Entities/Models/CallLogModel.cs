using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WaxWelio.Entities.Models
{
    public class CallLogModel:BaseModel
    {
        [JsonProperty("appointmentId")]
        public string AppointmentId { get; set; }

        [JsonProperty("startDate")]
        public string StartDate { get; set; }

        [JsonProperty("endDate")]
        public string EndDate { get; set; }

        [JsonProperty("fee")]
        public int Fee { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("durationInMin")]
        public string DurationMin { get; set; }

        public string StrStatus { get; set; }

        public string StrStartDate { get; set; }

        public string StrEndDate { get; set; }
    }
}
