namespace Alpha.MemberInfo.Features.CustomerList
{
    using Alpha.MemberInfo.Features.CustomerList.Criteria;
    using Alpha.MemberInfo.Features.CustomerList.Models;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.CustomerList.Params;
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

        CustomerStatusModel GetCustomerStatus(int custId);

        Result UpdateCustomerStatus(UpdateCustomerStatusParam param);
    }

    public class CustomerListServiceImpl : CustomerListService
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly CustomerListDataAccess customerListDataAccess;

        public CustomerListServiceImpl(
            IDynamicRepository dynamicRepository,
            CustomerListDataAccess customerListDataAccess)
        {
            this.dynamicRepository = dynamicRepository;
            this.customerListDataAccess = customerListDataAccess;
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

        public CustomerStatusModel GetCustomerStatus(int custId)
          => customerListDataAccess.GetCustomerStatus(new SelectCustomerAllStatusModelCriteria(custId));


        public Result UpdateCustomerStatus(UpdateCustomerStatusParam param)
            => customerListDataAccess.UpdateCustomerStatus(new UpdateCustomerStatusCommand(
                   param.SearchAccountId,
                   param.ActorId,
                   param.SubAccountId,
                   param.Language,
                   param.IP,
                   param.StatusName,
                   param.StatusValue));
    }
}
