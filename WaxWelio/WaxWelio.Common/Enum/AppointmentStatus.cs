using System.ComponentModel;

namespace WaxWelio.Common.Enum
{
    public enum AppointmentStatus
    {
        [Description("Cancelled")]
        Cancel = 0,

        [Description("New")]
        New = 1,

        [Description("In progress")]
        InProgress = 2,

        [Description("Finished")]
        Finished = 3,
    }
}