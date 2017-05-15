namespace WaxWelio.Entities.Models
{
    class CallTimeModel
    {
        public string AppoinmentId { get; set; }

        public long StartTime { get; set; }

        public long EndTime { get; set; }

        public int Minutes { get; set; }

        public int Seconds { get; set; }
    }
}
