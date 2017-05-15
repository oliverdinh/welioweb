using System.ComponentModel;

namespace WaxWelio.Common.Enum
{
    public enum CallStatus
    {
        [Description("Call - No answer")]
        NoAnswer = 3,
        
        [Description("Call - Started")]
        Ready = 1,

        [Description("Call - Failed")]
        Progress = 8,

        [Description("Call - Finished")]
        Finished = 9
    }
}