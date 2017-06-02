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

        Task<Result> UpdateCashBalanceTransferAsync(
            int custId,
            decimal amount,
            int userId,
            int subAccId,
            string ip);
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

        public async Task<Result> UpdateCashBalanceTransferAsync(
            int custId,
            decimal amount,
            int userId,
            int subAccId,
            string ip)
        {
            var criteria = new UpdateCashBalanceTransferCriteria(custId, amount, userId, subAccId, ip);

            await repository.ExecuteAsync(criteria);

            return new Result
            {
                Code = criteria.ErrCode,
                Message = criteria.ErrMsg
            };
        }
    }
}
