namespace Alpha.MemberInfo.Features.CustomerList
{
    using Alpha.MemberInfo.Features.CustomerList.Criteria;
    using Alpha.MemberInfo.Features.CustomerList.Models;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.CustomerList.Params;
    using Alpha.MemberInfo.Site.Features.CustomerList.ViewModels;
    using Fanex.Data.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface CustomerListService
    {
        CustomerListDTO GetCustomerList(CustomerListParam param);

        CustomerStatusModel GetCustomerStatus(int custId);

        Result UpdateCustomerStatus(UpdateCustomerStatusParam param);

    }

    public class CustomerListServiceImpl : CustomerListService
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly CustomerListDataAccess customerListDataAccess;
        private readonly IDictionary<UserLevel, Func<CustomerListParam, CustomerListDTO>> fetchData;

        public CustomerListServiceImpl(
            IDynamicRepository dynamicRepository,
            CustomerListDataAccess customerListDataAccess)
            : this(dynamicRepository, customerListDataAccess, null)
        {
        }

        internal CustomerListServiceImpl(
            IDynamicRepository dynamicRepository,
            CustomerListDataAccess customerListDataAccess,
            IDictionary<UserLevel, Func<CustomerListParam, CustomerListDTO>> fetchData)
        {
            this.dynamicRepository = dynamicRepository;
            this.customerListDataAccess = customerListDataAccess;
            this.fetchData = fetchData ??
                  new Dictionary<UserLevel, Func<CustomerListParam, CustomerListDTO>>
                {
                    {UserLevel.Super, FectchMasterList},
                    {UserLevel.Master, FetchAgentList},
                    {UserLevel.Agent, FetchMemberList}
                };
        }

        public CustomerListDTO GetCustomerList(CustomerListParam param)
            => fetchData.ContainsKey(param.DirectUplineCustLevel) ? fetchData[param.DirectUplineCustLevel](param) : 
                new CustomerListDTO(Enumerable.Empty<CustomerListModelBase>(), 0, 0);

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

        internal CustomerListDTO FectchMasterList(CustomerListParam param)
        {
            var criteria = new SelectMasterListCriteria(
              param.DirectUplineId,
              param.Username,
              param.Enabled,
              param.Closed,
              param.Suspended,
              param.Locked,
              param.DoubleCommLosing,
              param.AllowAutoTransfer,
              param.PageSize,
              param.PageIndex);

            var model = dynamicRepository.Fetch<MasterListModel>(criteria);

            return new CustomerListDTO(model, criteria.TotalItems, criteria.TotalPages);
        }

        internal CustomerListDTO FetchAgentList(CustomerListParam param)
        {
            var criteria = new SelectAgentListCriteria(
                param.UserId,
                param.DirectUplineId,
                param.Username,
                param.Enabled,
                param.Closed,
                param.Suspended,
                param.Locked,
                param.DoubleCommLosing,
                param.AllowAutoTransfer,
                param.PageSize,
                param.PageIndex);

            var model = dynamicRepository.Fetch<AgentListModel>(criteria);

            return new CustomerListDTO(model, criteria.TotalItems, criteria.TotalPages);
        }

        internal CustomerListDTO FetchMemberList(CustomerListParam param)
        {
            var criteria = new SelectMemberListCriteria(
                param.UserId,
                param.DirectUplineId,
                param.Username,
                param.Enabled,
                param.Closed,
                param.Suspended,
                param.Locked,
                param.DoubleCommLosing,
                param.AllowAutoTransfer,
                param.PageSize,
                param.PageIndex);

            var model = dynamicRepository.Fetch<MemberListModel>(criteria);

            return new CustomerListDTO(model, criteria.TotalItems, criteria.TotalPages);
        }
    }
}