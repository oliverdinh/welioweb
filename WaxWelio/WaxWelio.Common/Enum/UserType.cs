using System.ComponentModel;

namespace WaxWelio.Common.Enum
{
    public enum UserType
    {
        [Description("Practice Manager")]
        PracticeManager = 1,

        [Description("Doctor")]
        Doctor = 2,

        [Description("Receptionist")]
        Receptionist = 3,
    }
}
