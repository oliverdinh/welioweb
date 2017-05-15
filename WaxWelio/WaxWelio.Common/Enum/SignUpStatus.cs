using System.ComponentModel;

namespace WaxWelio.Common.Enum
{
    public enum SignUpStatus
    {
        [Description("Pending approval")] Pending = 1,

        [Description("Approved")] Approved = 2,

        [Description("Declined")] Declined = 0,

        [Description("Closed")] Closed = 3
    }
}