using System.Collections.Generic;
using WaxWelio.Common.Config;
using WaxWelio.Common.Object;
using WaxWelio.Entities.Models;
using WaxWelio.Entities.Result;

namespace WaxWelio.Abstractions
{
    public interface IPatientService : IBaseAbstraction<PatientModel>
    {
        PatientResult GetByPhone(string phoneNumber);

        IList<PatientModel> ListPatients(int start = GlobalConstant.StartIndex, int length = GlobalConstant.Length, string search = null);

        int Total();

        void UpdatePatient(UpdatePatientModel model);
    }
}