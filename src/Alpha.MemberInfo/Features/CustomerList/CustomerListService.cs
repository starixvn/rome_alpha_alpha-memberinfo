namespace Alpha.MemberInfo.Features.CustomerList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Alpha.MemberInfo.Features.CustomerList.Criteria;
    using Alpha.MemberInfo.Features.CustomerList.Models;
    using Alpha.MemberInfo.Features.CustomerList.SPs.Criteria;
    using Alpha.MemberInfo.Features.Shared.Models;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Services.Transfer;
    using Alpha.MemberInfo.Site.Features.CustomerList.Params;
    using Alpha.MemberInfo.Site.Features.CustomerList.ViewModels;
    using Fanex.Data.Repository;

    public interface CustomerListService
    {
        CustomerListDTO GetCustomerList(CustomerListParam param);

        Task<CustomerStatusModel> GetCustomerStatus(int custId, byte roleId, int uplineId);

        Result UpdateCustomerStatus(UpdateCustomerStatusParam param);
    }

    public class CustomerListServiceImpl : CustomerListService
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly IDictionary<UserLevel, Func<CustomerListParam, CustomerListDTO>> fetchData;
        private readonly TransferService transferService;

        public CustomerListServiceImpl(IDynamicRepository dynamicRepository, TransferService transferService)
            : this(dynamicRepository, transferService, null)
        {
        }

        internal CustomerListServiceImpl(
            IDynamicRepository dynamicRepository,
            TransferService transferService,
            IDictionary<UserLevel, Func<CustomerListParam, CustomerListDTO>> fetchData)
        {
            this.dynamicRepository = dynamicRepository;
            this.transferService = transferService;
            this.fetchData = fetchData ??
                new Dictionary<UserLevel, Func<CustomerListParam, CustomerListDTO>>
                {
                    [UserLevel.Super] = FectchMasterList,
                    [UserLevel.Master] = FetchAgentList,
                    [UserLevel.Agent] = FetchMemberList
                };
        }

        public CustomerListDTO GetCustomerList(CustomerListParam param)
        {
            if (param.IsDirectMember)
            {
                return FetchMemberList(param);
            }

            return fetchData.ContainsKey(param.DirectUplineCustLevel) ? fetchData[param.DirectUplineCustLevel](param) :
                new CustomerListDTO(Enumerable.Empty<CustomerListModelBase>(), 0, 0);
        }

        public async Task<CustomerStatusModel> GetCustomerStatus(int custId, byte roleId, int uplineId)
        {
            var (customerStatus, dcsProducts)
                = await dynamicRepository.FetchMultipleAsync<NonProductStatusModel, DCSProductStatusModel>(
                    new SelectCustomerAllStatusModelCriteria(custId));

            var customerStatusModel = new CustomerStatusModel(
                customerStatus.FirstOrDefault(),
                dcsProducts
                    .GroupBy(product => product.ProductId)
                    .Select(products =>
                    {
                        foreach (var product in products)
                        {
                            if (!product.IsHidden)
                            {
                                return product;
                            }
                        }

                        return products.First();
                    }));

            if (customerStatusModel.NonProduct != null)
            {
                var closedReasonAndIsProtectedMode = await dynamicRepository.GetAsync<ClosedReasonAndIsProtectedMode>(
                    new SelectClosedReasonCriteria(custId, roleId));
                customerStatusModel.NonProduct.IsProtectedMode = closedReasonAndIsProtectedMode != null
                    && closedReasonAndIsProtectedMode.IsProtectedMode;

                var scheduleOptionCommand = new SelectScheduleOptionCommand(custId);
                await dynamicRepository.ExecuteAsync(scheduleOptionCommand);
                customerStatusModel.NonProduct.IsAllowAutoTransfer = scheduleOptionCommand.IsAllowAutoTransfer;
                customerStatusModel.NonProduct.ScheduleWeekDays = scheduleOptionCommand.ScheduleWeekDays;
                customerStatusModel.NonProduct.TransferOption = await transferService.GetTransferOption(uplineId);
            }

            return customerStatusModel;
        }

        public Result UpdateCustomerStatus(UpdateCustomerStatusParam param)
        {
            UpdateCustomerStatusCommand command = null;
            switch (param.StatusName)
            {
                case StatusNames.Closed:
                    command = new UpdateClosedStatusCommand(
                        param.SearchAccountId,
                        param.ActorId,
                        param.SubAccountId,
                        param.Language,
                        param.IP,
                        param.StatusName,
                        param.StatusValue);
                    break;

                case StatusNames.DisableAutoMasterPT:
                    command = new UpdateDisableAutoMPTStatusCommand(
                        param.SearchAccountId,
                        param.ActorId,
                        param.SubAccountId,
                        param.Language,
                        param.IP,
                        param.StatusName,
                        param.StatusValue);
                    break;

                case StatusNames.DoubleComm:
                    command = new UpdateDoubleCommLosingStatusCommand(
                        param.SearchAccountId,
                        param.ActorId,
                        param.SubAccountId,
                        param.Language,
                        param.IP,
                        param.StatusName,
                        param.StatusValue);
                    break;

                case StatusNames.DisableOutright:
                    command = new UpdateDisableOutrightStatusCommand(
                        param.SearchAccountId,
                        param.ActorId,
                        param.SubAccountId,
                        param.Language,
                        param.IP,
                        param.StatusName,
                        param.StatusValue);
                    break;

                case StatusNames.Suspended:
                    command = new UpdateSuspendedStatusCommand(
                        param.SearchAccountId,
                        param.ActorId,
                        param.SubAccountId,
                        param.Language,
                        param.IP,
                        param.StatusName,
                        param.StatusValue);
                    break;

                case StatusNames.Unlock:
                    command = new UpdateUnlockedCommand(
                        param.SearchAccountId,
                        param.ActorId,
                        param.SubAccountId,
                        param.Language,
                        param.IP,
                        param.StatusName);
                    break;

                case StatusNames.AllowAutoTransfer:
                    command = new UpdateAllowAutoTransferStatusCommand(
                        param.SearchAccountId,
                        param.ActorId,
                        param.SubAccountId,
                        param.Language,
                        param.IP,
                        param.StatusName,
                        param.StatusValue);
                    break;

                default:
                    return new Result();
            }

            dynamicRepository.Execute(command);

            return new Result { Code = command.ErrCode, Message = command.ErrMsg };
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