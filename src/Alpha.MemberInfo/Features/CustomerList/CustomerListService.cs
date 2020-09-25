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

    public interface CustomerListService
    {
        IEnumerable<int> GetUnsyncedProductIds(int custId);

        CustomerListDTO GetCustomerList(CustomerListParam param);

        CustomerStatusModel GetCustomerStatus(int custId);

        Result UpdateCustomerStatus(UpdateCustomerStatusParam param);

        IEnumerable<SyncedCasinoModel> GetSyncedCasinoModel(string custIds);
    }

    public class CustomerListServiceImpl : CustomerListService
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly CustomerListDataAccess customerListDataAccess;
        private readonly IDictionary<UserLevel, Func<CustomerListParam, CustomerListDTO>> buildCriteria;

        public CustomerListServiceImpl(
            IDynamicRepository dynamicRepository,
            CustomerListDataAccess customerListDataAccess)
            : this(dynamicRepository, customerListDataAccess, null)
        {
        }

        internal CustomerListServiceImpl(
            IDynamicRepository dynamicRepository,
            CustomerListDataAccess customerListDataAccess,
            IDictionary<UserLevel, Func<CustomerListParam, CustomerListDTO>> buildCriteria)
        {
            this.dynamicRepository = dynamicRepository;
            this.customerListDataAccess = customerListDataAccess;
            this.buildCriteria = buildCriteria ??
                  new Dictionary<UserLevel, Func<CustomerListParam, CustomerListDTO>>
                {
                    {UserLevel.Super, BuildMasterListCriteria},
                    {UserLevel.Master, BuildAgentListCriteria},
                    {UserLevel.Agent, BuildMemberListCriteria}
                };
        }

        public IEnumerable<int> GetUnsyncedProductIds(int custId)
            => dynamicRepository.Fetch<int>(new SelectUnsyncProductsCriteria(custId));

        public CustomerListDTO GetCustomerList(CustomerListParam param)
            => buildCriteria[param.DirectUplineCustLevel](param);

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

        public IEnumerable<SyncedCasinoModel> GetSyncedCasinoModel(string custIds)
            => dynamicRepository.Fetch<SyncedCasinoModel>(new SelectSyncedCasinoList(custIds));

        internal CustomerListDTO BuildMasterListCriteria(CustomerListParam param)
        {
            var criteria = new SelectMasterListCriteria(
              param.DirectUplineId,
              param.Username,
              param.Enabled,
              param.Closed,
              param.Suspended,
              param.DoubleCommLosing,
              param.ProductUnSync,
              param.PageSize,
              param.PageIndex);

            var model = dynamicRepository.Fetch<MasterListModel>(criteria);

            return new CustomerListDTO(model, criteria.TotalItems, criteria.TotalPages, param.LimitedDateToShowInfo);
        }

        internal CustomerListDTO BuildAgentListCriteria(CustomerListParam param)
        {
            var criteria = new SelectAgentListCriteria(
                param.UserId,
                param.DirectUplineId,
                param.Username,
                param.Enabled,
                param.Closed,
                param.Suspended,
                param.DoubleCommLosing,
                param.ProductUnSync,
                param.PageSize,
                param.PageIndex);

            var model = dynamicRepository.Fetch<AgentListModel>(criteria);

            return new CustomerListDTO(model, criteria.TotalItems, criteria.TotalPages, param.LimitedDateToShowInfo);
        }

        internal CustomerListDTO BuildMemberListCriteria(CustomerListParam param)
        {
            var criteria = new SelectMemberListCriteria(
                param.UserId,
                param.DirectUplineId,
                param.Username,
                param.Enabled,
                param.Closed,
                param.Suspended,
                param.DoubleCommLosing,
                param.ProductUnSync,
                param.PageSize,
                param.PageIndex);

            var model = dynamicRepository.Fetch<MemberListModel>(criteria);

            return new CustomerListDTO(model, criteria.TotalItems, criteria.TotalPages, param.LimitedDateToShowInfo);
        }
    }
}
