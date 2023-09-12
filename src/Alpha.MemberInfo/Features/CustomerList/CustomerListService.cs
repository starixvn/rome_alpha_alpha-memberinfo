namespace Alpha.MemberInfo.Features.CustomerList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Alpha.MemberInfo.Features.CustomerList.Criteria;
    using Alpha.MemberInfo.Features.CustomerList.Models;
    using Alpha.MemberInfo.Features.CustomerList.SPs.Criteria;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.CustomerList.Params;
    using Alpha.MemberInfo.Site.Features.CustomerList.ViewModels;
    using Fanex.Data.Repository;

    public interface CustomerListService
    {
        CustomerListDTO GetCustomerList(CustomerListParam param);

        CustomerStatusModel GetCustomerStatus(int custId, byte roleId);

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

        public CustomerStatusModel GetCustomerStatus(int custId, byte roleId)
        {
            var customerStatus = customerListDataAccess.GetCustomerStatus(new SelectCustomerAllStatusModelCriteria(custId));

            if (customerStatus != null && customerStatus.NonProduct != null)
            {
                customerStatus.NonProduct.IsProtectedMode = customerListDataAccess.GetIsProtectedMode(new SelectClosedReasonCriteria(custId, roleId));
            }

            return customerStatus;
        }

        public Result UpdateCustomerStatus(UpdateCustomerStatusParam param)
        {
            NonQueryCommand command = null;
            switch (param.StatusName)
            {
                case StatusNames.Closed:
                    command = new UpdateClosedStatusCommand(param);
                    break;
                case StatusNames.DisableAutoMasterPT:
                    command = new UpdateDisableAutoMPTStatusCommand(param);
                    break;
                case StatusNames.DoubleComm:
                    command = new UpdateDoubleCommLosingStatusCommand(param);
                    break;
                case StatusNames.DisableOutright:
                    command = new UpdateDisableOutrightStatusCommand(param);
                    break;
                case StatusNames.Suspended:
                    command = new UpdateSuspendedStatusCommand(param);
                    break;
                case StatusNames.Unlock:
                    command = new UpdateUnlockedCommand(param);
                    break;
                case StatusNames.AllowAutoTransfer:
                    command = new UpdateAllowAutoTransferStatusCommand(param);
                    break;
                default:                 
                    break;
            }
            return customerListDataAccess.UpdateCustomerStatus(command);
        }

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