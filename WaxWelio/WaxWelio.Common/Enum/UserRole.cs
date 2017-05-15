using System.ComponentModel;

namespace WaxWelio.Common.Enum
{
    public enum UserRole
    {
        [Description("User")] User = 1,

        [Description("Administrator")] Administrator = 9,
    }
}