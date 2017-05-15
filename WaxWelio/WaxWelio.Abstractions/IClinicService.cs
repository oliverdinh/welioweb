
using System.Collections.Generic;
using WaxWelio.Common.Config;
using WaxWelio.Entities.Models;
using WaxWelio.Entities.Result;

namespace WaxWelio.Abstractions
{
    public interface IClinicService : IBaseAbstraction<ClinicResult>
    {
        IList<ClinicResult> GetListClinic(string type, string orderByName, string keywords, int start = GlobalConstant.StartIndex, int length = GlobalConstant.Length);

        ClinicResult GetDetails(string id);

        ClinicResult Update(ClinicModel model);
        int Total();
    }
}
