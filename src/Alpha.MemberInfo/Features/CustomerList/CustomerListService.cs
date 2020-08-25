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
        CustomerListDTO GetMasterList(MasterListParam param);

        CustomerListDTO GetAgentList(AgentListParam param);

        CustomerListDTO GetMemberList(MemberListParam param);

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

        public CustomerListDTO GetMasterList(MasterListParam param)
        {
            var criteria = new SelectMasterListCriteria(
                param.Srecommend,
                param.Username,
                param.Enabled,
                param.Closed,
                param.Suspended,
                param.DoubleCommLosing,
                param.ProductUnSync,
                param.PageSize,
                param.PageIndex);
            
            var model = dynamicRepository.Fetch<MasterListModel>(criteria).OrderBy(item => item.RowNumber);

            return new CustomerListDTO(model, criteria.TotalItems, criteria.TotalPages, param.LimitedDateToShowInfo);
        }

        public CustomerListDTO GetAgentList(AgentListParam param)
        {
            var criteria = new SelectAgentListCriteria(
                param.UserId,
                param.Mrecommend,
                param.Username,
                param.Enabled,
                param.Closed,
                param.Suspended,
                param.DoubleCommLosing,
                param.ProductUnSync,
                param.PageSize,
                param.PageIndex);

            var model = dynamicRepository.Fetch<MasterListModel>(criteria).OrderBy(item => item.RowNumber);

            return new CustomerListDTO(model, criteria.TotalItems, criteria.TotalPages, param.LimitedDateToShowInfo);
        }

        public CustomerListDTO GetMemberList(MemberListParam param)
        {
            var criteria = new SelectMemberListCriteria(
                param.UserId,
                param.Recommend,
                param.Username,
                param.Enabled,
                param.Closed,
                param.Suspended,
                param.DoubleCommLosing,
                param.ProductUnSync,
                param.PageSize,
                param.PageIndex);

            var model = dynamicRepository.Fetch<MemberListModel>(criteria).OrderBy(item => item.RowNumber);

            return new CustomerListDTO(model, criteria.TotalItems, criteria.TotalPages, param.LimitedDateToShowInfo);
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
