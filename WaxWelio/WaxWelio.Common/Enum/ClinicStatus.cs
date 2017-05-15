using System.ComponentModel;

namespace WaxWelio.Common.Enum
{
    public enum ClinicStatus
    {
        [Description("Pending Approval")]
        PendingApproval = 1,

        [Description("Approved")]
        Approved = 2,

        [Description("Closed")]
        Closed = 3,

        [Description("Declined")]
        Declined = 0
    }
}
