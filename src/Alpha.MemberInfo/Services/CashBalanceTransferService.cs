using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha.MemberInfo.Criteria.Transfer;
using Alpha.MemberInfo.Models;
using Alpha.MemberInfo.Models.Transfer;
using Fanex.Data.Repository;

namespace Alpha.MemberInfo.Services
{
    public interface CashBalanceTransferService
    {
        Task<CashBalanceTransfer> GetCashBalanceTransferAsync(int custId);

        Task<Result> AddCashBalanceTransferAsync(
            int uplineId,
            int custId,
            decimal amount,
            string superIdsTransferTotal,
            int subAccId,
            string ip,
            string languageKey,
            bool isAllDayBalTransfer,
            UserLevel currentUserLevel);
    }

    public class CashBalanceTransferServiceImpl : CashBalanceTransferService
    {
        private readonly IDynamicRepository repository;

        public CashBalanceTransferServiceImpl(IDynamicRepository dynamicRepository)
        {
            repository = dynamicRepository;
        }

        public async Task<CashBalanceTransfer> GetCashBalanceTransferAsync(int custId)
        {
            ICriteria criteria = new GetCashBalanceTransferCriteria(custId);

            return await repository.GetAsync<CashBalanceTransfer>(criteria);
        }

        public async Task<Result> AddCashBalanceTransferAsync(
            int uplineId,
            int custId,
            decimal amount,
            string superIdsTransferTotal,
            int subAccId,
            string ip,
            string languageKey,
            bool isAllDayBalTransfer,
            UserLevel currentUserLevel)
        {
            var criteria = GetAddCashBalanceTransferCriteria(
                uplineId,
                custId,
                amount,
                superIdsTransferTotal,
                subAccId,
                ip,
                languageKey,
                isAllDayBalTransfer,
                currentUserLevel);

            await repository.ExecuteAsync(criteria);

            return new Result
            {
                Code = criteria.ErrCode,
                Message = criteria.ErrCode == 0 ? AppResources.Transfer.TransferSuccessfully : criteria.ErrMsg
            };
        }

        private AddCashBalanceTransferCriteria GetAddCashBalanceTransferCriteria(
            int uplineId,
            int custId,
            decimal amount,
            string superIdsTransferTotal,
            int subAccId,
            string ip,
            string languageKey,
            bool isAllDayBalTransfer,
            UserLevel currentUserLevel)
        {
            if (currentUserLevel.IsSuperLevel())
            {
                return new AddMasterCashBalanceTransferCriteria(
                    uplineId,
                    custId,
                    amount,
                    superIdsTransferTotal,
                    subAccId,
                    ip,
                    languageKey);
            }

            if (currentUserLevel.IsMasterLevel())
            {
                return new AddAgentCashBalanceTransferCriteria(
                    uplineId,
                    custId,
                    amount,
                    superIdsTransferTotal,
                    subAccId,
                    ip,
                    languageKey);
            }

            if (currentUserLevel.IsAgentLevel())
            {
                return new AddMemberCashBalanceTransferCriteria(
                    uplineId,
                    custId,
                    amount,
                    superIdsTransferTotal,
                    subAccId,
                    ip,
                    languageKey,
                    isAllDayBalTransfer);
            }

            throw new InvalidOperationException("Invalid UserLevel.");
        }
    }
}
