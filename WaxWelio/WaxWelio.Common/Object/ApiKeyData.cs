namespace WaxWelio.Common.Object
{
    public class ApiKeyData
    {
        private readonly string _key;

        public static readonly ApiKeyData ListHospital = new ApiKeyData("hospitals");
        public static readonly ApiKeyData Pricing = new ApiKeyData("Pricings");
        public static readonly ApiKeyData Doctor = new ApiKeyData("Doctor");
        public static readonly ApiKeyData ListDoctor = new ApiKeyData("Doctors");
        public static readonly ApiKeyData User = new ApiKeyData("user");
        public static readonly ApiKeyData Patient = new ApiKeyData("Patient");
        public static readonly ApiKeyData Appointment = new ApiKeyData("appointment");
        public static readonly ApiKeyData ListAppointment = new ApiKeyData("Appointments");
        public static readonly ApiKeyData UserId = new ApiKeyData("userId");
        public static readonly ApiKeyData Hospital = new ApiKeyData("hospital");
        public static readonly ApiKeyData BankAccount = new ApiKeyData("Payment");
        public static readonly ApiKeyData ListPatient = new ApiKeyData("Patients");
        public static readonly ApiKeyData Profile = new ApiKeyData("profile");
        public static readonly ApiKeyData AppointmentCount = new ApiKeyData("appointmentCount");
        public static readonly ApiKeyData Clinics = new ApiKeyData("Clinics");
        public static readonly ApiKeyData Status = new ApiKeyData("Status");

        private ApiKeyData(string key)
        {
            _key = key;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        protected bool Equals(ApiKeyData other)
        {
            return string.Equals(_key, other._key);
        }

        public override int GetHashCode()
        {
            return _key?.GetHashCode() ?? 0;
        }

        public override string ToString()
        {
            return _key;
        }
    }
}