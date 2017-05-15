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
    public class PriceService : IPriceService
    {
        public PriceModel AddOrUpdate(ApiHeader apiHeader, PriceModel entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(ApiHeader apiHeader, string id)
        {
            throw new NotImplementedException();
        }

        public PriceModel GetById(ApiHeader apiHeader, string id)
        {
            throw new NotImplementedException();
        }

        public IList<PriceModel> GetAll(ApiHeader apiHeader)
        {
            throw new NotImplementedException();
        }

        public IList<PriceModel> Get(ApiHeader apiHeader, int start = GlobalConstant.StartIndex, int length = GlobalConstant.Length,
            string searchKeyword = null, SortType sortType = GlobalConstant.DefaultSortType)
        {
            throw new NotImplementedException();

        }

        public IList<PriceModel> Get(string clinicId)
        {
            var url = ApiUrl.Default.RootApi + string.Format(ApiUrl.Default.ListPricing, clinicId);
            return Restful.Get(url, null).GetList<PriceModel>(ApiKeyData.Pricing);
        }

        public IList<PriceModel> Search(ApiHeader apiHeader, string hospitalId, int start = GlobalConstant.StartIndex,
            int length = GlobalConstant.Length, string searchKeyword = null, SortField sortField = SortField.None,
            SortType sortType = SortType.Desc, object status = null)
        {
            throw new NotImplementedException();
        }

        public void AddPrice(ApiHeader apiHeader, EditPriceModel models)
        {
            var url = ApiUrl.Default.RootApi + ApiUrl.Default.AddPrice;
            //var ob = JsonConvert.SerializeObject(models);
            Restful.Post(url, apiHeader, models);
        }

        public void AddOrEditBank(BankModel model)
        {
            var url = ApiUrl.Default.RootApi + ApiUrl.Default.AddOrEditBankAccount;
            Restful.Post(url, null, model);
        }

        public BankModel GetBankAccount(string clinicId)
        {
            var url = ApiUrl.Default.RootApi + string.Format(ApiUrl.Default.GetBankAccount, clinicId);
            return Restful.Get(url, null).Get<BankModel>(ApiKeyData.BankAccount);
        }
    }
}
