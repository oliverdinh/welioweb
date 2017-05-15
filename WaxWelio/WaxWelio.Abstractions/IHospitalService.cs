using System.Collections.Generic;
using WaxWelio.Common.Config;
using WaxWelio.Common.Enum;
using WaxWelio.Common.Object;
using WaxWelio.Entities.Models;

namespace WaxWelio.Abstractions
{
    public interface IHospitalService : IBaseAbstraction<HospitalModel>
    {
        int Total();
        void SignUp(SignUpModel model);

        void ApproveClinic(ApiHeader apiHeader, string id);

        void CheckExpired(ApiHeader apiHeader, string hash, string email);

        ApiHeader ActiveUser(ApiHeader apiHeader, CreateNewPasswordModel model);

        void CreatePassword(ApiHeader apiHeader, CreateNewPasswordModel model);

        IList<HospitalModel> ListHospitals(ApiHeader apiHeader, string status = "all", string signUpStatus = null, int start = GlobalConstant.StartIndex,
            int length = GlobalConstant.Length, bool showAdmin = false, string search = null, bool searchExactly = false);

        void ResendActiveEmail(ApiHeader apiHeader, string email);

        void ResendInviteEmail(string userId, string clinicId);

        void NotifyClinic(ApiHeader apiHeader, NotifyClinicModel model);
    }
}