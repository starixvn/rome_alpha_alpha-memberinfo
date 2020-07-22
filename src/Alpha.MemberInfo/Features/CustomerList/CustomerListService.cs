namespace Alpha.MemberInfo.Features.QRCodeMemberSignIn
{
    using Alpha.MemberInfo.Features.CustomerList.Criteria;
    using Alpha.MemberInfo.Features.CustomerList.Models;
    using Alpha.MemberInfo.Site.Features.CustomerList.ViewModels;
    using Fanex.Data.Repository;
    using System.Linq;

    public interface CustomerListService
    {
        CustomerListDTO GetMasterList(
            int srecommend,
            string username,
            bool? enabled,
            bool? closed,
            bool? suspended,
            byte doubleCommLosing,
            byte productUnSync,
            int pageSize,
            int pageIndex);

        CustomerListDTO GetAgentList(
            int userId,
            int mrecommend,
            string username,
            bool? enabled,
            bool? closed,
            bool? suspended,
            byte doubleCommLosing,
            byte productUnSync,
            int pageSize,
            int pageIndex);

        CustomerListDTO GetMemberList(
           int userId,
           int recommend,
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

        public CustomerListDTO GetMasterList(
            int srecommend,
            string username,
            bool? enabled,
            bool? closed,
            bool? suspended,
            byte doubleCommLosing,
            byte productUnSync,
            int pageSize,
            int pageIndex)
        {
            var criteria = new SelectMasterListCriteria(
                   srecommend,
             username,
             enabled,
             closed,
             suspended,
             doubleCommLosing,
             productUnSync,
             pageSize,
             pageIndex);
            
            var model = dynamicRepository.Fetch<MasterListModel>(criteria).OrderBy(item => item.RowNumber);

            return new CustomerListDTO(model, criteria.TotalItems, criteria.TotalPages);
        }

        public CustomerListDTO GetAgentList(
            int userId,
           int mrecommend,
           string username,
           bool? enabled,
           bool? closed,
           bool? suspended,
           byte doubleCommLosing,
           byte productUnSync,
           int pageSize,
           int pageIndex)
        {
            var criteria = new SelectAgentListCriteria(
                userId,
                 mrecommend,
           username,
           enabled,
           closed,
           suspended,
           doubleCommLosing,
           productUnSync,
           pageSize,
           pageIndex);

            var model = dynamicRepository.Fetch<MasterListModel>(criteria).OrderBy(item => item.RowNumber);

            return new CustomerListDTO(model, criteria.TotalItems, criteria.TotalPages);
        }


        public CustomerListDTO GetMemberList(
           int userId,
           int recommend,
           string username,
           bool? enabled,
           bool? closed,
           bool? suspended,
           byte doubleCommLosing,
           byte productUnSync,
           int pageSize,
           int pageIndex)
        {
            var criteria = new SelectMemberListCriteria(
              userId,
              recommend,
              username,
              enabled,
              closed,
              suspended,
              doubleCommLosing,
              productUnSync,
              pageSize,
              pageIndex);

          var model = dynamicRepository.Fetch<MemberListModel>(criteria).OrderBy(item => item.RowNumber);

            return new CustomerListDTO(model, criteria.TotalItems, criteria.TotalPages);
        }
    }
}
