using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha.MemberInfo.AppResources;
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
            int fromCustId,
            int toCustId,
            decimal amount,
            int creator,
            int subAccId,
            string ip,
            string languageKey);

        Task<string> GetAllowTransfer(int custId);
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
            int fromCustId,
            int toCustId,
            decimal amount,
            int creator,
            int subAccId,
            string ip,
            string languageKey)
        {
            amount *= -1;

            var criteria = new AddCashBalanceTransferCriteria(
                fromCustId,
                toCustId,
                amount,
                creator,
                subAccId,
                ip,
                languageKey);

            await repository.ExecuteAsync(criteria);

            return new Result
            {
                Code = criteria.ErrCode,
                Message = criteria.ErrCode == 0 ? Transfer.TransferSuccessfully : criteria.ErrMsg
            };
        }

        public async Task<string> GetAllowTransfer(int custId)
        {
            string transferOption = await GetTransferOption(custId);

            string transferTime = Transfer.AllowTransfer;
            char[] charArray = transferOption.ToCharArray();

            if (charArray.Length > 0)
            {
                StringBuilder dayBuilder = new StringBuilder();
                string[] dayArray =
                {
                    Transfer.Sunday,
                    Transfer.Monday,
                    Transfer.Tuesday,
                    Transfer.Wednesday,
                    Transfer.Thursday,
                    Transfer.Friday,
                    Transfer.Saturday
                };

                for (int i = 1, max = charArray.Length; i < max - 1; i++)
                {
                    if (charArray[i] == '1')
                    {
                        dayBuilder.Append(" - ").Append(dayArray[i]);
                    }
                }

                if (charArray[0] == '1')
                {
                    dayBuilder.Append(" - ").Append(dayArray[0]);
                }

                string day = dayBuilder.ToString();

                if (day.Length > 0)
                {
                    day = day.Substring(3);
                }

                transferTime += charArray[7] == '1' ?
                    " " + day :
                    " " + Transfer.Daily;
            }

            return transferTime;
        }

        private async Task<string> GetTransferOption(int custId)
        {
            var criteria = new GetTransferOptionCriteria(custId);

            await repository.ExecuteAsync(criteria);

            return criteria.TransferOption;
        }
    }
}
