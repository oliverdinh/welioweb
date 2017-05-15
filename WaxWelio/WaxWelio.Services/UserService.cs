using System;
using System.Collections.Generic;

using WaxWelio.Abstractions;
using WaxWelio.Common;
using WaxWelio.Common.Config;
using WaxWelio.Common.Enum;
using WaxWelio.Common.Object;
using WaxWelio.Entities;
using WaxWelio.Entities.Models;
using WaxWelio.Entities.Result;

namespace WaxWelio.Services
{
    public class UserService : IUserService
    {
        public UserModel AddOrUpdate(ApiHeader apiHeader, UserModel entity)
        {
            throw new NotImplementedException();
        }

        public KeyValueResult[] Add(ApiHeader apiHeader, UserModel entity)
        {
            //var url = ApiUrl.Default.RootApi + ApiUrl.Default.AddOrUpdateUser;
            //var data = Restful.PostMultipartForm(url, apiHeader, entity);
            //KeyValueResult[] result = new KeyValueResult[2];
            //result[0] = new KeyValueResult("userId", data["userId"]);
            //result[1] = new KeyValueResult("isExist", data["isExist"]);
            //return result;
            return null;
        }

        public void Delete(ApiHeader apiHeader, string id)
        {
            throw new NotImplementedException();
        }

        public UserModel GetById(ApiHeader apiHeader, string id)
        {
            throw new NotImplementedException();
        }

        public IList<UserModel> GetAll(ApiHeader apiHeader)
        {
            throw new NotImplementedException();
        }

        public IList<UserModel> Get(ApiHeader apiHeader, int start = GlobalConstant.StartIndex,
            int length = GlobalConstant.Length,
            string searchKeyword = null, SortType sortType = GlobalConstant.DefaultSortType)
        {
            throw new NotImplementedException();
        }

        public IList<UserModel> Search(ApiHeader apiHeader, string hospitalId, int start = GlobalConstant.StartIndex,
            int length = GlobalConstant.Length, string searchKeyword = null, SortField sortField = SortField.None,
            SortType sortType = SortType.Desc, object status = null)
        {
            throw new NotImplementedException();
        }

        public ApiHeader Login(LoginModel model)
        {
            ////var url = ApiUrl.Default.SystemLogin;
            ////var data = Restful.Post(url, null, model);
            ////return data.Get<ApiHeader>();
            //return new ApiHeader
            //{
            //    UserId = "54e522a7-bbbb-11e6-bec5-7f2e9609e38d",
            //    SessionId = "fc2a9f05-2030-11e7-9dad-03aab276124f",
            //    SessionExpired = 1492092441926,
            //    AppType = 1,
            //    UserType = 2,
            //    Hospitals = new List<Entities.Result.UserHospital>
            //    {
            //        new Entities.Result.UserHospital
            //        {
            //            HospitalId = "54e522a8-bbbb-11e6-bec5-b95b951517af",
            //            Name = "Clinic 1",
            //            Photos = new Entities.Result.Photos
            //            {
            //                Logo = "http://portalvhdsmt4cfkhpy3dw.blob.core.windows.net/pictures/welio/hospitals/default-hospital-icon.jpg"
            //            },
            //            Roles = new List<int>{ 9 },
            //            SubType = 1,
            //            Status = 1
            //        }
            //    },
            //    User = new UserModel
            //    {
            //        FirstName = "John",
            //        LastName = "Doe",
            //        Email = "johndoe@gmail.com",
            //        PhoneNumber = "+841289810988",
            //        HospitalId = "54e522a8-bbbb-11e6-bec5-b95b951517af",
            //        UserRole = 9,
            //        UserSubType = 1,
            //        Status = 1,
            //        DoctorInfos = new[] 
            //        {
            //            new DoctorInfoModel
            //            {
            //                Roles = new int[] {9},
            //                SubType = 1,
            //                Status = 1,
            //                Hospital = new HospitalModel
            //                {
            //                    ClinicName = "Abc",
            //                    AddressLine1 = "Line 1",
            //                    PostCode = "1234",
            //                    Suburb = "6583475",
            //                    Id = "54e522a8-bbbb-11e6-bec5-b95b951517af",
            //                    PhoneNumber = new List<string> { "+841289810988" },
            //                    SignUpStatus = 1
            //                }
            //            }
            //        }
            //    },
            //};
            return null;
        }

        public IList<UserClinicModel> ListUsers(string clinicId, int start, int lenght, string orderType, string keyword)
        {
            var url = ApiUrl.Default.RootApi + ApiUrl.Default.ListClinicUser;
            var data = Restful.Post(url, null, new
            {
                ClinicId = clinicId,
                Start = start,
                Limit = lenght,
                OrderByType = orderType,
                OrderByKey = "DoctorName"
            });
            _total = data["Total"].ToObject<int>();
            return data.GetList<UserClinicModel>(ApiKeyData.ListDoctor);
        }

        public IList<UserModel> ListClinicUsers(int start, int lenght, string keyword)
        {
            var url = ApiUrl.Default.RootApi + ApiUrl.Default.ListUser;
            var data = Restful.Post(url, null, new {
                Start = start,
                Limit = lenght,
                Keywords = keyword,
            });
            _total = data["Total"].ToObject<int>();
            return data.GetList<UserModel>(ApiKeyData.ListDoctor);
        }

        public UserModel GetProfile(ApiHeader apiHeader, string id)
        {
            var url = ApiUrl.Default.RootApi + string.Format(ApiUrl.Default.GetUserProfile, id);
            var data = Restful.Get(url, apiHeader);
            UserModel result = data.Get<UserModel>(ApiKeyData.User);
            result.AppointmentCount = (string)data["appointmentCount"];
            return result;
        }

        private void CreatePassword(ApiHeader apiHeader, CreateNewPasswordModel model)
        {
            var url = ApiUrl.Default.RootApi + ApiUrl.Default.ChangePassword;
            Restful.Post(url, apiHeader, model);
        }

        public void ConfirmInviteNewUser(ApiHeader apiHeader, InviteNewUserModel model)
        {
            //var url = ApiUrl.Default.RootApi + ApiUrl.Default.ConfirmInvitationNewUser;
            //Restful.PostMultipartForm(url, apiHeader, model.User);

            ////neeus ok
            //CheckExpired(model.User.Email, model.User.Hash, true, model.Password.Password);
        }

        public void ConfirmInviteUser(ApiHeader apiHeader, string hash, string email)
        {
            var url = ApiUrl.Default.RootApi + string.Format(ApiUrl.Default.ConfirmInvitationUser, hash, email);
            Restful.Get(url, apiHeader);
        }

        public CreateClinicUserResult CreateOtherUser(CreateClinicUserModel model)
        {
            var url = ApiUrl.Default.RootApi + ApiUrl.Default.CreateClinicUser;
            return Restful.PostMultipartForm(url, null, model).Get<CreateClinicUserResult>();
        }

        public void UpdateOtherUser(UpdateClinicUserModel model)
        {
            var url = ApiUrl.Default.RootApi + ApiUrl.Default.EditClinicUser;
            Restful.PostMultipartForm(url, null, model).Get<UserModel>(ApiKeyData.User);
        }

        public void ForgotPassword(string email)
        {
            var url = ApiUrl.Default.RootApi + string.Format(ApiUrl.Default.ForgotPassword, email);
            Restful.Get(url);
        }

        public void ActiveUserInvite(string email, string hash, string clinicId)
        {
            var url = ApiUrl.Default.RootApi + ApiUrl.Default.ChangeStatusUserClinic;
            Restful.Post(url, null, new
            {
                Email = email,
                HashCode = hash,
                ClinicId = clinicId,
                Status = 1,
                Type = "ACTIVE"
            });
        }

        public ApiHeader CheckExpired(string email, string hash, bool updatePassword, string password)
        {
            //var data = Restful.Get(ApiUrl.Default.RootApi + string.Format(ApiUrl.Default.CheckExpired, hash, email));
            //var apiHeader = data.Get<ApiHeader>();
            //if (updatePassword)
            //    SetPassword(apiHeader, password);
            //return new ApiHeader
            //{
            //    UserId = "54e522a7-bbbb-11e6-bec5-7f2e9609e38d",
            //    SessionId = "283824ff-1f67-11e7-9dad-393113c8f9f0",
            //    SessionExpired = 1499999999999,
            //    AppType = 1,
            //    UserType = 2,
            //    Hospitals = new List<Entities.Result.UserHospital>
            //    {
            //        new Entities.Result.UserHospital
            //        {
            //            HospitalId = "54e522a8-bbbb-11e6-bec5-b95b951517af",
            //            Name = "Clinic 1",
            //            Photos = new Entities.Result.Photos
            //            {
            //                Logo = "http://portalvhdsmt4cfkhpy3dw.blob.core.windows.net/pictures/welio/hospitals/default-hospital-icon.jpg"
            //            },
            //            Roles = new List<int>{ 9 },
            //            SubType = 1,
            //            Status = 1
            //        }
            //    },
            //    User = new UserModel
            //    {
            //        FirstName = "John",
            //        LastName = "Doe",
            //        Email = "johndoe@gmail.com",
            //        PhoneNumber = "+841289810988",
            //        HospitalId = "54e522a8-bbbb-11e6-bec5-b95b951517af",
            //        UserRole = 9,
            //        UserSubType = 1,
            //        Status = 1,
            //        DoctorInfos = new[]
            //        {
            //            new DoctorInfoModel
            //            {
            //                Roles = new int[] {9},
            //                SubType = 1,
            //                Status = 1,
            //                Hospital = new HospitalModel
            //                {
            //                    ClinicName = "Abc",
            //                    AddressLine1 = "Line 1",
            //                    PostCode = "1234",
            //                    Suburb = "6583475",
            //                    Id = "54e522a8-bbbb-11e6-bec5-b95b951517af",
            //                    PhoneNumber = new List<string> { "+841289810988" },
            //                    SignUpStatus = 1
            //                }
            //            }
            //        }
            //    },
            //}; ;
            return null;
        }

        public UserModel CheckExpiredAndGetUser(string email, string hash)
        {
            var data = Restful.Get(ApiUrl.Default.RootApi + string.Format(ApiUrl.Default.CheckExpired, hash, email));
            var user = data.Get<UserModel>(ApiKeyData.Profile);
            return user;
        }

        public void SetPassword(ApiHeader apiHeader, string password)
        {
            var url = ApiUrl.Default.RootApi + ApiUrl.Default.SetPasswordForSignUp;
            Restful.Post(url, apiHeader, new { newPassword = password });
        }

        public void CloseUnconfirmAccount(ApiHeader apiHeader, string userId, string hospitalId)
        {
            var url = ApiUrl.Default.RootApi + string.Format(ApiUrl.Default.CloseUnconfirmAccount, userId, hospitalId);
            Restful.Get(url, apiHeader);
        }

        public void ReOpenAccount(ApiHeader apiHeader, string userId, string hospitalId)
        {
            var url = ApiUrl.Default.RootApi + string.Format(ApiUrl.Default.ReOpenAccount, userId, hospitalId);
            Restful.Get(url, apiHeader);
        }

        public StatusResult CheckDoctorExists(string clinicId, string email)
        {
            var url = ApiUrl.Default.RootApi + ApiUrl.Default.CheckDoctorExists;
            var data = new
            {
                ClinicId = clinicId,
                Email = email
            };
            return Restful.Post(url, null, data).Get<StatusResult>();
        }

        public HospitalModel GetClinicInforForPartner(string lang, string hospitalId)
        {
            var url = ApiUrl.Default.RootApi + string.Format(ApiUrl.Default.GetClinicInforForPartner, hospitalId);
            ApiHeader apiHeader = new ApiHeader();
            apiHeader.Lang = lang;
            apiHeader.Token = GlobalConstant.TokenForParner;
            return Restful.Get(url, apiHeader).Get<HospitalModel>(ApiKeyData.Hospital);
        }

        private int _total;
        public int Total()
        {
            return _total;
        }

        public AuthInfo GetAuthInfo(string email)
        {
            var url = ApiUrl.Default.RootApi + ApiUrl.Default.LoginDoctor;
            var data = new
            {
                Email365 = email
            };
            return Restful.Post(url, null, data).Get<AuthInfo>();
        }

        public string getEmail365(string token)
        {
            var result = Restful.getEmail365(token);
            return result.Email;
        }

        public IList<DoctorResult> GetDoctorsByClinic(string clinicId)
        {
            var url = ApiUrl.Default.RootApi + string.Format(ApiUrl.Default.GetDoctorsByClinic, clinicId);
            return Restful.Get(url, null).GetList<DoctorResult>(ApiKeyData.ListDoctor);
        }

        public UserModel GetUserDetails(string id)
        {
            var url = ApiUrl.Default.RootApi + string.Format(ApiUrl.Default.GetUserDetails, id);
            return Restful.Get(url, null).Get<UserModel>(ApiKeyData.Doctor);
        }

        public void ChangeStatusUser(string email, string clinicId, int otherAccount, long timeCurrent, int status, string statusType)
        {
            var url = ApiUrl.Default.RootApi + ApiUrl.Default.ChangeStatusUserClinic;
            var data = new
            {
                Email = email,
                ClinicId = clinicId,
                OtherAccount = otherAccount,
                TimeCurrent = timeCurrent,
                Status = status,
                Type = statusType
            };
            Restful.Post(url, null, data);
        }

        public StatusResult CheckUpcomingAppointmentsOfDoctor(string doctorId, string clinicId, long timeCurrent)
        {
            var url = ApiUrl.Default.RootApi + ApiUrl.Default.CheckUpcomingAppointmentsOfDoctor;
            var data = new
            {
                DoctorId = doctorId,
                ClinicId = clinicId,
                TimeCurrent = timeCurrent,
            };
            return Restful.Post(url, null, data).Get<StatusResult>();
        }

        public string CreateOffice365User(string email, string displayName, string password, string token)
        {
            var result = Restful.CreateUserOffice365(email, displayName, password, token);
            return result.Error == null ? "ok" : "Your email already exists";
        }

        public void SendMailAccount365(string email, string firstname, string email365, string password365)
        {
            var url = ApiUrl.Default.RootApi + ApiUrl.Default.SendOffice365ToUser;
            var data = new {
                FirstName = firstname,
                Email = email,
                Email365 = email365,
                Password365 = password365
            };
            Restful.Post(url, null, data);
        }

        public void UpdateAccountOffice365(string doctorId, string email365, string password365)
        {
            var url = ApiUrl.Default.RootApi + ApiUrl.Default.UpdateAccountOffice365;
            var data = new
            {
                DoctorId = doctorId,
                Email365 = email365,
                Password365 = password365
            };
            Restful.Post(url, null, data);
        }

        public void ActiveUser(string doctorId, int status, string clinicId)
        {
            var url = ApiUrl.Default.RootApi + ApiUrl.Default.ActiveDoctor;
            var data = new
            {
                DoctorId = doctorId,
                Status = status,
                ClinicId = clinicId
            };
            Restful.Post(url, null, data);
        }
    }
}