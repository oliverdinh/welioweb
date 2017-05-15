using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WaxWelio.Abstractions;
using WaxWelio.Common;
using WaxWelio.Common.Config;
using WaxWelio.Common.Enum;
using WaxWelio.Common.Object;
using WaxWelio.Entities.Models;
using WaxWelio.Entities.Result;

namespace WaxWelio.Services
{
    public class AppointmentService : IAppointmentService
    {
        public AppointmentResult AddOrUpdate(ApiHeader apiHeader, AppointmentResult entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(ApiHeader apiHeader, string id)
        {
            throw new NotImplementedException();
        }

        public AppointmentResult GetById(ApiHeader apiHeader, string id)
        {
            var url = ApiUrl.Default.RootApi + string.Format(ApiUrl.Default.GetAppointmentById, id);
            return Restful.Get(url, apiHeader).Get<AppointmentResult>(ApiKeyData.Appointment);
        }

        public IList<AppointmentResult> GetAll(ApiHeader apiHeader)
        {
            throw new NotImplementedException();
        }

        public IList<AppointmentResult> Get(ApiHeader apiHeader, int start = GlobalConstant.StartIndex,
            int length = GlobalConstant.Length,
            string searchKeyword = null, SortType sortType = GlobalConstant.DefaultSortType)
        {
            throw new NotImplementedException();
        }

        public IList<AppointmentResult> Search(ApiHeader apiHeader, string hospitalId,
            int start = GlobalConstant.StartIndex,
            int length = GlobalConstant.Length, string searchKeyword = null, SortField sortField = SortField.None,
            SortType sortType = SortType.Desc, object status = null)
        {
            throw new NotImplementedException();
        }

        private int _total;
        public int Total()
        {
            return _total;
        }

        public IList<AppointmentResult> Get(ApiHeader apiHeader, string hospitalId, string doctorId, string status,
            long? startDate, long? endDate, int start, int lenght, string keyword, bool searchExactly, SortField orderBy, SortType sortType)
        {
            var url = ApiUrl.Default.RootApi +
                      string.Format(ApiUrl.Default.ListAppointment, hospitalId, doctorId, status, startDate, endDate,
                          start, lenght, keyword, searchExactly, orderBy.DescriptionAttr(), sortType.DescriptionAttr());
            var data = Restful.Get(url, apiHeader);
            _total = data["total"].ToObject<int>();
            return data.GetList<AppointmentResult>(ApiKeyData.ListAppointment);
        }

        public void CreateAppointment(CreateUpdateAppointmentModel model)
        {
            var url = ApiUrl.Default.RootApi + ApiUrl.Default.CreateNormalingAppointment;
            Restful.Post(url, null, model);
        }

        public void UpdateAppointment(CreateUpdateAppointmentModel model)
        {
            var url = ApiUrl.Default.RootApi + ApiUrl.Default.UpdateAppointment;
            Restful.Post(url, null, model);
        }

        public void CancelAppointment(CancelAppoinmentModel model)
        {
            var url = ApiUrl.Default.RootApi + ApiUrl.Default.CancelAppointment;
            Restful.Post(url, null, model);
        }

        public string PushNotifiCall(ApiHeader apiHeader, string id)
        {
            var url = ApiUrl.Default.RootApi + string.Format(ApiUrl.Default.PushNotifiCall, id);
            var result = Restful.Get(url, apiHeader);
            return result["callId"].ToString();
        }

        public void FinishCall(ApiHeader apiHeader, string id, int status)
        {
            var url = ApiUrl.Default.RootApi + string.Format(ApiUrl.Default.CallStatus, id, status);
            Restful.Get(url, apiHeader);
        }

        public void UpdateStatus(string id, int status)
        {
            var url = ApiUrl.Default.RootApi + ApiUrl.Default.UpateAppointmentStatus;
            var data = new
            {
                AppointmentId = id,
                Status = status
            };
            Restful.Post(url, null, data);
        }

        public IList<AppointmentResult> GetListAppointment(FilterAppointmentModel model)
        {
            var url = ApiUrl.Default.RootApi + ApiUrl.Default.GetListAppointmentsOfDoctor;
            var data = Restful.Post(url, null, model);
            _total = data["Total"].ToObject<int>();
            return data.GetList<AppointmentResult>(ApiKeyData.ListAppointment);
        }
    }
}