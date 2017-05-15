namespace WaxWelio.Entities.Models
{
    public class ClinicForUserModel : BaseModel
    {
        public string ClinicName { get; set; }

        public int UserType { get; set; }

        public int Access { get; set; }
    }
}
