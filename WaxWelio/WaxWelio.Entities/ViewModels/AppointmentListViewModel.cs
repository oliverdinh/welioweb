using System.Collections.Generic;
using WaxWelio.Entities.Models;

namespace WaxWelio.Entities.ViewModels
{
    public class AppointmentListViewModel : BaseModel
    {
        public string Date { get; set; }

        public string Time { get; set; }

        public string DoctorName { get; set; }

        public string PatientName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Status { get; set; }

        public int Duration { get; set; }

        public string OtherFamilyMember { get; set; }

        public string OtherFirstName { get; set; }

        public string OtherLastName { get; set; }

        public string Phone { get; set; }

        public string ActualStatus { get; set; }

        public string Fee { get; set; }

        public string ActualStart { get; set; }

        public string ActualDuration { get; set; }

        public string ActualFee { get; set; }

        public string SipUriMeeting { get; set; }

        public string UrlJoinMeeting { get; set; }

        public string AppointmentDate { get; set; }

        public bool HasCarer { get; set; }

        public bool IsFloating { get; set; }

        public string Email { get; set; }

        public string DoctorId { get; set; }

        public string PatientId { get; set; }

        public string PatientAvatar { get; set; }

        public long? ExpectedStartDateTime { get; set; }

        public IList<CallLogModel> CallLog { get; set; }
    }
}