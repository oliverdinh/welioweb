using System.Collections.Generic;

namespace WaxWelio.Common.Exception
{
    public class ApiErrorMapping
    {
        private readonly Dictionary<string, string> _errorDictionary = new Dictionary<string, string>
        {
            {"-1", "Unexpected Error"},
            {"-2", "Unexpected Error"},
            {"-3", "User disabled"},
            {"1", "Invalid Parameter"},
            {"2", "Invalid Session"},
            {"3", "Your office 365 email does not exist in Welio or has been changed. Please contact with administrator to get more information."},
            {"4", "Object not found"},
            {"5", "Permission deny"},
            {"7", "Failed invalid phone number"},
            {"8", "Failed invalid email"},
            {"9", "Failed user not found"},
            {"11", "Your phone number already exists in Welio."},
            {"12", "Your email address already exists in Welio."},
            {"13", "Failed user existed"},
            {"15", "Failed invalid location"},
            {"16", "Failed hospital not found"},
            {"17", "Failed username exist"},
            {"22", "Failed address empty"},
            {"23", "Failed photo empty"},
            {"24", "Failed firstname empty"},
            {"25", "Failed lastname empty"},
            {"26", "Failed gender empty"},
            {"27", "Failed birthday empty"},
            {"28", "Failed username empty"},
            {"29", "Failed password empty"},
            {"30", "Failed hospital empty"},
            {"33", "Failed invalid start date"},
            {"34", "Failed invalid end date"},
            {"44", "Failed doctor not found"},
            {"83", "Failed invalid old password"},
            {"82", "Failed invalid password lenght"},
            {"112", "Failed invalid date"},
            {"181", "Failed state empty"},
            {"182", "Failed post code empty"},
            {"183", "Failed phone empty"},
            {"184", "Failed invalid hospital phone"},
            {"185", "Failed patient not found"},
            {"186", "Failed invalid duration"},
            {"187", "Failed price not set"},
            {"188", "Failed invalid patient fee"},
            {"189", "Failed invalid welio fee"},
            {"190", "Failed duration existed"},
            {"191", "Failed SIP Uri empty"},
            {"192", "Failed url join meeting empty"},
            {"193", "Failed account name empty"},
            {"194", "Failed account name empty"},
            {"197", "This address already exists in Welio. The clinics address is used to uniquely identify it on Welio. Please check that your clinic does not already have a Welio account, or contact Welio for support." },
            {"199", "This phone number already exists in Welio. The clinics phone number is used to uniquely identify it on Welio. Please check that your clinic does not already have a Welio account, or contact Welio for support." },
            {"200", "You cannot close this account as this is the only user with Administrator access" },
            {"89", "The patient’s payment details are invalid. They must update their payment details in the WELIO app before this appointment can be created" },
            {"50", "Appointment date invalid" },
            {"49", "Failed doctor not empty." },
            {"201", "Cannot close an account linked to appointment(s) in progress. Please finish all appointment(s) in progress first." },
        };
        public ApiErrorMapping(string errorCode, string errorDesc)
        {
            _errorCode = errorCode;
            _errorDesc = errorDesc;
        }

        private readonly string _errorCode;
        private readonly string _errorDesc;

        public string Message => _errorDictionary.ContainsKey(_errorCode) ? _errorDictionary[_errorCode] : _errorDesc;
    }
}