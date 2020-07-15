namespace Alpha.MemberInfo.Features.QRCodeMemberSignIn
{
    using Alpha.MemberInfo.Features.CustomerList.Criteria;
    using Alpha.MemberInfo.Features.CustomerList.Models;
    using Fanex.Data.Repository;
    using System.Collections.Generic;
    using System.Linq;

    public interface CustomerListService
    {
        IEnumerable<MasterListModel> GetMasterList(
            int srecommend,
            string username,
            bool? enabled,
            bool? closed,
            bool? suspended,
            byte doubleCommLosing,
            byte productUnSync,
            int pageSize,
            int pageIndex);
    }

    public class CustomerListServiceImpl : CustomerListService
    {
        private readonly IDynamicRepository dynamicRepository;

        public CustomerListServiceImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public IEnumerable<MasterListModel> GetMasterList(
            int srecommend,
            string username,
            bool? enabled,
            bool? closed,
            bool? suspended,
            byte doubleCommLosing,
            byte productUnSync,
            int pageSize,
            int pageIndex)
            => dynamicRepository.Fetch<MasterListModel>(
              new SelectMasterListCriteria(
                   srecommend,
             username,
             enabled,
             closed,
             suspended,
             doubleCommLosing,
             productUnSync,
             pageSize,
             pageIndex)).OrderBy(item => item.RowNumber);
    }
}
