using WaxWelio.Entities.Models;

namespace WaxWelio.Entities.ViewModels
{
    public class CreateAppointmentViewModel : BaseModel
    {
        public bool IsFloating { get; set; }

        public bool IsCarer { get; set; }

        public bool IsNewPatient { get; set; }

        public string PatientId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string OtherFirstName { get; set; }

        public string OtherLastName { get; set; }

        public string DoctorId { get; set; }

        public string DoctorName { get; set; }

        public string ExpectedDate { get; set; }

        public string ExpectedTime { get; set; }

        public string Duration { get; set; }

        public string SipUriMeeting { get; set; }

        public string UrlJoinMeeting { get; set; }

        public bool IsNormaling { get; set; }
    }
}
