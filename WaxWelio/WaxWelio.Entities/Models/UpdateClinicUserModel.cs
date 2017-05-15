namespace WaxWelio.Entities.Models
{
    public class UpdateClinicUserModel
    {
        public string ClinicUserId { get; set; }

        public string DoctorId { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int UserType { get; set; }

        public int AccessType { get; set; }

        public string FileName { get; set; }

        public byte[] Imagefile { get; set; }
    }
}
