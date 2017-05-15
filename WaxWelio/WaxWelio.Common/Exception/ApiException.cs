using System.Threading;

namespace WaxWelio.Common.Exception
{
    public class ApiException : System.Exception
    {
        public ApiException(string errorDesc) : base(errorDesc)
        {
            ErrorCode = "-1";
            ErrorDesc = errorDesc;
        }

        public ApiException(string errorDesc, string errorCode) : base(errorDesc)
        {
            ErrorCode = errorCode;
            ErrorDesc = errorDesc;
        }

        public ApiException(string errorDesc, string errorCode, string cultureCode) : base(errorDesc)
        {
            ErrorCode = errorCode;
            ErrorDesc = errorDesc;
            CultureCode = cultureCode;
        }

        public string ErrorCode { get; }
        public string ErrorDesc { get; set; }
        private string CultureCode { get; set; }

        public override string Message
        {
            get
            {
                CultureCode = Thread.CurrentThread.CurrentCulture.Name;
                return new ApiErrorMapping(ErrorCode, ErrorDesc).Message;
            }
        }
    }
}