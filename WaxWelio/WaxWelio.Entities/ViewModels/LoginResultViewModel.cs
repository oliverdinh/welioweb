using System.Collections.Generic;
using WaxWelio.Entities.Result;

namespace WaxWelio.Entities.ViewModels
{
    public class LoginResultViewModel
    {
        public bool IsSuccess { get; set; }

        public int UserType { get; set; }

        public List<UserHospital> Hospitals { get; set; }

        public string Error { get; set; }
    }
}