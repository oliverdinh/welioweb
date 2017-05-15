using System;
using System.Collections.Generic;
using System.Linq;
using WaxWelio.Abstractions;
using WaxWelio.Common;
using WaxWelio.Common.Config;
using WaxWelio.Common.Enum;
using WaxWelio.Common.Object;
using WaxWelio.Entities.Models;
using WaxWelio.Entities.Result;

namespace WaxWelio.Services
{
    public class ClinicService : IClinicService
    {
        private int _total;

        public int Total()
        {
            return _total;
        }

        public ClinicResult AddOrUpdate(ApiHeader apiHeader, ClinicResult entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(ApiHeader apiHeader, string id)
        {
            throw new NotImplementedException();
        }

        public IList<ClinicResult> Get(ApiHeader apiHeader, int start = 0, int length = int.MaxValue, string searchKeyword = null, SortType sortType = SortType.Desc)
        {
            throw new NotImplementedException();
        }

        public IList<ClinicResult> GetAll(ApiHeader apiHeader)
        {
            throw new NotImplementedException();
        }

        public ClinicResult GetById(ApiHeader apiHeader, string id)
        {
            throw new NotImplementedException();
        }

        public IList<ClinicResult> GetListClinic(string type, string orderByName, string keyWords, int start = 0, int length = int.MaxValue)
        {
            var url = ApiUrl.Default.RootApi + string.Format(ApiUrl.Default.GetListClinic);
            var data = new
            {
                Type = type,
                Start = start,
                Limit = length,
                OrderByName = string.IsNullOrEmpty(orderByName) ? "" : orderByName.ToUpper(),
                Keywords = string.IsNullOrEmpty(keyWords) ? "" : keyWords
            };
            var result = Restful.Post(url, null, data);
            _total = result["Total"].ToObject<int>();
            return result.GetList<ClinicResult>(ApiKeyData.Clinics);
        }

        public ClinicResult GetDetails(string id)
        {
            var url = ApiUrl.Default.RootApi + string.Format(ApiUrl.Default.GetClinicDetails, id);
            return Restful.Get(url, null).Get<ClinicResult>();
        }

        public ClinicResult Update(ClinicModel model)
        {
            var url = ApiUrl.Default.RootApi + string.Format(ApiUrl.Default.UpdateClinic);
            return Restful.Post(url, null, model).Get<ClinicResult>();
        }

        public IList<ClinicResult> Search(ApiHeader apiHeader, string hospitalId, int start = 0, int length = int.MaxValue, string searchKeyword = null, SortField sortField = SortField.None, SortType sortType = SortType.Desc, object status = null)
        {
            throw new NotImplementedException();
        }
    }
}
