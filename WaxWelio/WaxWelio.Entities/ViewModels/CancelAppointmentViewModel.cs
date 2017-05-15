namespace WaxWelio.Entities.ViewModels
{
    public class CancelAppointmentViewModel
    {
        public string AppointmentId { get; set; }

        public string DoctorName { get; set; }

        public long? ExpectedStartDateTime { get; set; }

        public string PatientFirstName { get; set; }

        public string PatientLastName { get; set; }

        public string PatientPhone { get; set; }

        public string PatientEmail { get; set; }
    }
}
