using System;
using System.Collections.Generic;
using WaxWelio.Common.Object;
using WaxWelio.Entities;
using WaxWelio.Entities.Models;
using WaxWelio.Entities.Result;

namespace WaxWelio.Abstractions
{
    public interface IUserService : IBaseAbstraction<UserModel>
    {

        ApiHeader Login(LoginModel model);

        IList<UserClinicModel> ListUsers(string clinicId, int start, int lenght, string orderType, string keyword);

        IList<UserModel> ListClinicUsers(int start, int lenght, string keyword);

        KeyValueResult[] Add(ApiHeader apiHeader, UserModel model);

        UserModel GetProfile(ApiHeader apiHeader, string id);

        void ConfirmInviteNewUser(ApiHeader apiHeader, InviteNewUserModel model);

        void ConfirmInviteUser(ApiHeader apiHeader, string hash, string email);

        CreateClinicUserResult CreateOtherUser(CreateClinicUserModel model);

        void UpdateOtherUser(UpdateClinicUserModel model);

        void ActiveUserInvite(string email, string hash, string clinicId);

        void ForgotPassword(string email);

        ApiHeader CheckExpired(string email, string hash, bool updatePassword = false, string password = null);

        void SetPassword(ApiHeader apiHeader, string password);

        void CloseUnconfirmAccount(ApiHeader apiHeader, string userId, string hospitalId);

        void ReOpenAccount(ApiHeader apiHeader, string userId, string hospitalId);

        UserModel CheckExpiredAndGetUser(string email, string hash);

        HospitalModel GetClinicInforForPartner(string lang, string hospitalId);

        int Total();

        AuthInfo GetAuthInfo(string email);

        string getEmail365(string token);

        IList<DoctorResult> GetDoctorsByClinic(string clinicId);

        UserModel GetUserDetails(string id);

        void ActiveUser(string doctorId, int status, string clinicId);

        void ChangeStatusUser(string email, string clinicId, int otherAccount, long timeCurrent, int status, string statusType);

        StatusResult CheckUpcomingAppointmentsOfDoctor(string doctorId, string clinicId, long timeCurrent);

        string CreateOffice365User(string email, string displayName, string password, string token);

        void SendMailAccount365(string email, string firstname, string email365, string password365);

        void UpdateAccountOffice365(string doctorId, string email365, string password365);

        StatusResult CheckDoctorExists(string clinicId, string email);
    }
}