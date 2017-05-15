using System;
using System.Web;
using WaxWelio.Common.Enum;

namespace WaxWelio.Common.Config
{
    public class GlobalConstant
    {
        public const int StartIndex = 0;
        public const int Length = int.MaxValue;
        public const int WelioFee = 2;
        public const int PriceStatus = 1;
        public const SortType DefaultSortType = SortType.Desc;
        public const string UrlLogin = "~/Home";
        public const string NoImage = "no-image.png";
        public const string SignInOffice365Url = "https://login.windows.net/common/oauth2/authorize?response_type=token&client_id=2bd937f8-d693-4e38-8718-2f53deed2dff&redirect_uri={0}/SkypeForBusiness/Login&resource=https://webdir.online.lync.com";
        public static string BaseUrl { get; } = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);

        public const string TokenForParner = "d29ed5b4bb9311e68fbdab778db9d578";

        public const string DateFormat = "dd MMM yyyy";
        public const string TimeFormat = "h:mm tt";
        public const string DateTimeFormat = "dd MMM yyyy hh:mm tt";
        
        public const string ErrorTemp = "ErrorTemp";
        public const string SessionExpiredOrNotLogin = "Session is expired or you have not logged in. Please try to login again.";
        public const string EmailExited = "Email Existed.";
        public const string ClinicPhoneExisted = "Clinic Phone Existed.";
        public const string UserPhoneExitsed = "User Phone Existed.";
        public const string ClinicAddressExisted = "Clinic Address Existed.";
        public const string InvalidLocation = "Please Enter Valid Location.";
        public const string EmailInviteNewUser = "An email has been sent inviting this user to WELIO";
        public const string EmailInviteExistUser = "An account for this email already exists. Their account details have been retrieved and an email has been sent inviting them to WELIO";
        public const string UserExisted = "An account for this username already exists. Please try again.";
        public const string UserModel = "UserModel";
        public const string SessionExpired = "Your session has expired. Please signin again.";
        public const string ClosedClinicUser = "You can not access to clinic because your account has been blocked.";
    }
}