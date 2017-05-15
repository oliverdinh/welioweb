using System;
using System.Collections.Generic;
using WaxWelio.Abstractions;
using WaxWelio.Common;
using WaxWelio.Common.Config;
using WaxWelio.Common.Enum;
using WaxWelio.Common.Object;
using WaxWelio.Entities.Models;
using WaxWelio.Entities.Result;

namespace WaxWelio.Services
{
    public class PatientService : IPatientService
    {
        private int _total;

        public PatientModel AddOrUpdate(ApiHeader apiHeader, PatientModel entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(ApiHeader apiHeader, string id)
        {
            throw new NotImplementedException();
        }

        public PatientModel GetById(ApiHeader apiHeader, string id)
        {
            var url = ApiUrl.Default.RootApi + string.Format(ApiUrl.Default.GetUserProfile, id);
            return Restful.Get(url, apiHeader).Get<PatientModel>(ApiKeyData.Patient);
        }

        public IList<PatientModel> GetAll(ApiHeader apiHeader)
        {
            throw new NotImplementedException();
        }

        public IList<PatientModel> Get(ApiHeader apiHeader, int start = GlobalConstant.StartIndex,
            int length = GlobalConstant.Length,
            string searchKeyword = null, SortType sortType = GlobalConstant.DefaultSortType)
        {
            throw new NotImplementedException();
        }

        public IList<PatientModel> Search(ApiHeader apiHeader, string hospitalId, int start = GlobalConstant.StartIndex,
            int length = GlobalConstant.Length, string searchKeyword = null, SortField sortField = SortField.None,
            SortType sortType = SortType.Desc, object status = null)
        {
            throw new NotImplementedException();
        }

        public PatientResult GetByPhone(string phoneNumber)
        {
            var url = ApiUrl.Default.RootApi + ApiUrl.Default.GetPatientByPhone;
            var data = new
            {
                Phone = phoneNumber
            };
            return Restful.Post(url, null, data).Get<PatientResult>();
        }

        public IList<PatientModel> ListPatients(int start = GlobalConstant.StartIndex, int length = GlobalConstant.Length, string search = null)
        {
            var url = ApiUrl.Default.RootApi + ApiUrl.Default.ListPatient;
            var data = Restful.Post(url, null, new
            {
                Start = start,
                Limit = length
            });
            _total = data["Total"].ToObject<int>();
            return data.GetList<PatientModel>(ApiKeyData.ListPatient);
        }
        
        public int Total()
        {
            return _total;
        }

        public void UpdatePatient(UpdatePatientModel model)
        {
            var url = ApiUrl.Default.RootApi + ApiUrl.Default.UpdatePatient; 
            Restful.PostMultipartFormPatient(url, null, model);
        }
    }
}