using System;
using System.Collections.Generic;
using WaxWelio.Abstractions;
using WaxWelio.Common;
using WaxWelio.Common.Config;
using WaxWelio.Common.Enum;
using WaxWelio.Common.Object;
using WaxWelio.Entities.Models;

namespace WaxWelio.Services
{
    public class HospitalService : IHospitalService
    {
        public HospitalModel AddOrUpdate(ApiHeader apiHeader, HospitalModel entity)
        {
            //Update Clinic
            var url = ApiUrl.Default.RootApi + ApiUrl.Default.EditClinic;
            return Restful.PostMultipartFormClinic(url, apiHeader, entity).Get<HospitalModel>(ApiKeyData.Hospital);
        }

        public void Delete(ApiHeader apiHeader, string id)
        {
            throw new NotImplementedException();
        }

        public HospitalModel GetById(ApiHeader apiHeader, string id)
        {
            var url = ApiUrl.Default.RootApi + string.Format(ApiUrl.Default.GetClinic, id);
            return Restful.Get(url, apiHeader).Get<HospitalModel>(ApiKeyData.Hospital);
        }

        public IList<HospitalModel> GetAll(ApiHeader apiHeader)
        {
            throw new NotImplementedException();
        }

        public IList<HospitalModel> Get(ApiHeader apiHeader, int start = GlobalConstant.StartIndex,
            int length = GlobalConstant.Length,
            string searchKeyword = null, SortType sortType = GlobalConstant.DefaultSortType)
        {
            throw new NotImplementedException();
        }

        public IList<HospitalModel> Search(ApiHeader apiHeader, string hospitalId, int start = GlobalConstant.StartIndex,
            int length = GlobalConstant.Length, string searchKeyword = null, SortField sortField = SortField.None,
            SortType sortType = SortType.Desc, object status = null)
        {
            throw new NotImplementedException();
        }

        public void SignUp(SignUpModel model)
        {
            var url = ApiUrl.Default.RootApi + ApiUrl.Default.SignUp;

            Restful.Post(url, null, model);
        }

        public void ApproveClinic(ApiHeader apiHeader, string id)
        {
            var url = ApiUrl.Default.RootApi + string.Format(ApiUrl.Default.ApproveClinic, id);

            Restful.Get(url, apiHeader);
        }

        public void CheckExpired(ApiHeader apiHeader, string hash, string email)
        {
            var url = ApiUrl.Default.RootApi + string.Format(ApiUrl.Default.CheckExpired, hash, email);
            Restful.Get(url, apiHeader);
        }

        public ApiHeader ActiveUser(ApiHeader apiHeader, CreateNewPasswordModel model)
        {
            var url = ApiUrl.Default.RootApi + string.Format(ApiUrl.Default.ActiveClinic, model.Hash, model.Email);
            var data = Restful.Get(url, apiHeader);
            ApiHeader header = data.Get<ApiHeader>();
            CreatePassword(header, model);
            return header;
        }

        public void CreatePassword(ApiHeader apiHeader, CreateNewPasswordModel model)
        {
            var url = ApiUrl.Default.RootApi + ApiUrl.Default.ChangePassword;
            Restful.Post(url, apiHeader, model);
        }

        public IList<HospitalModel> ListHospitals(ApiHeader apiHeader, string status = "all", string signUpStatus = null,
            int start = GlobalConstant.StartIndex, int length = GlobalConstant.Length, bool showAdmin = false,
            string search = null, bool searchExactly = true)
        {
            var url = ApiUrl.Default.RootApi +
                      string.Format(ApiUrl.Default.ListHospital, status, signUpStatus, start, length, showAdmin, searchExactly, search);
            var data = Restful.Get(url, apiHeader);
            _total = data["total"].ToObject<int>();
            return data.GetList<HospitalModel>(ApiKeyData.ListHospital);
        }

        private int _total;
        public int Total()
        {
            return _total;
        }

        public void ResendActiveEmail(ApiHeader apiHeader, string email)
        {
            var url = ApiUrl.Default.RootApi + string.Format(ApiUrl.Default.ResendActiveEmail, email);
            Restful.Get(url, apiHeader);
        }

        public void ResendInviteEmail(string userId, string clinicId)
        {
            var url = ApiUrl.Default.RootApi + ApiUrl.Default.ResendInviteEmail;
            var data = new
            {
                ClinicId = clinicId,
                DoctorId = userId
            };
            Restful.Post(url, null, data);
        }

        public void NotifyClinic(ApiHeader apiHeader, NotifyClinicModel model)
        {
            var url = ApiUrl.Default.RootApi + ApiUrl.Default.NotifyClinic;
            Restful.Post(url, apiHeader, model);
        }
    }
}