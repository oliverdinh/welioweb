namespace WaxWelio.Entities.Models
{
    public class CreateClinicUserModel
    {
        public string ClinicId { get; set; }

        public string ClinicName { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int UserType { get; set; }

        public int AccessType { get; set; }

        public string FileName { get; set; }

        public byte[] Images { get; set; }

        public string Email { get; set; }
    }
}
