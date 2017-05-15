using System.Collections.Generic;
using WaxWelio.Common.Config;
using WaxWelio.Common.Enum;
using WaxWelio.Common.Object;

namespace WaxWelio.Abstractions
{
    public interface IBaseAbstraction<T>
    {
        T AddOrUpdate(ApiHeader apiHeader, T entity);
        void Delete(ApiHeader apiHeader, string id);
        T GetById(ApiHeader apiHeader, string id);
        IList<T> GetAll(ApiHeader apiHeader);

        IList<T> Get(ApiHeader apiHeader,
            int start = GlobalConstant.StartIndex,
            int length = GlobalConstant.Length,
            string searchKeyword = null,
            SortType sortType = GlobalConstant.DefaultSortType);

        IList<T> Search(ApiHeader apiHeader,
            string hospitalId,
            int start = GlobalConstant.StartIndex,
            int length = GlobalConstant.Length,
            string searchKeyword = null,
            SortField sortField = SortField.None,
            SortType sortType = SortType.Desc, object status = null);
    }
}