using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.Data.Repository;

namespace Alpha.MemberInfo.Criteria.Transfer
{
    public class AddCashBalanceTransferCriteria : NonQueryCommand
    {
        public AddCashBalanceTransferCriteria(
            int fromCustId,
            int toCustId,
            decimal amount,
            int creator,
            int subAccId,
            string ip,
            string languageKey)
        {
            FromCustId = fromCustId;
            ToCustId = toCustId;
            Amount = amount;
            Creator = creator;
            SubAccId = subAccId;
            IP = ip;
            Language = languageKey;
            ErrCode = 0;
            ErrMsg = string.Empty;
        }

        public int FromCustId { get; }

        public int ToCustId { get; }

        public decimal Amount { get; }

        public int Creator { get; }

        public int SubAccId { get; }

        public string IP { get; set; }

        public string Language { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey() => "Age_Transfer_CashBalance_Ins";

        public override bool IsValid() =>
            FromCustId > 0 &&
            ToCustId > 0 &&
            Creator > 0 &&
            !string.IsNullOrEmpty(IP);
    }
}
