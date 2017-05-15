using System.ComponentModel;

namespace WaxWelio.Common.Enum
{
    public enum SortField
    {
        [Description("")] None,
        [Description("expectedStartDate")] ExpectedStartDate,
        [Description("doctorId")] Doctor,
        [Description("patientId")] Patient,
        [Description("status")] Status,
    }
}