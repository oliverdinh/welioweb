using System.ComponentModel;

namespace WaxWelio.Common.Enum
{
    public enum Status
    {
        [Description("Active")]
        Enabled = 1,

        [Description("Unconfirmed")]
        Disabled = 0,

        [Description("Closed")]
        Closed = -1
    }
}