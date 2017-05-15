using System.Collections.Generic;
using WaxWelio.Common.Enum;
using WaxWelio.Common.Object;
using WaxWelio.Entities.Models;
using WaxWelio.Entities.Result;

namespace WaxWelio.Abstractions
{
    public interface IAppointmentService : IBaseAbstraction<AppointmentResult>
    {
        int Total();

        IList<AppointmentResult> Get(ApiHeader apiHeader, string hospitalId, string doctorId, string status,
            long? startDate, long? endDate, int start, int lenght, string keyword, bool searchExactly, SortField orderBy,
            SortType sortType);

        void CreateAppointment(CreateUpdateAppointmentModel model);

        string PushNotifiCall(ApiHeader apiHeader, string id);
        void FinishCall(ApiHeader apiHeader, string id, int status);

        void UpdateStatus(string id, int status);

        void UpdateAppointment(CreateUpdateAppointmentModel model);

        void CancelAppointment(CancelAppoinmentModel model);

        IList<AppointmentResult> GetListAppointment(FilterAppointmentModel model);
    }
}