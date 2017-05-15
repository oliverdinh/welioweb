using System.Collections.Generic;
using WaxWelio.Common.Config;
using WaxWelio.Common.Enum;
using WaxWelio.Common.Object;
using WaxWelio.Entities.Models;

namespace WaxWelio.Abstractions
{
    public interface IPriceService : IBaseAbstraction<PriceModel>
    {
        IList<PriceModel> Get(string clinicId);

        void AddPrice(ApiHeader apiHeader, EditPriceModel models);

        void AddOrEditBank(BankModel model);

        BankModel GetBankAccount(string clinicId);
    }
}