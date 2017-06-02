using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.Data.Repository;

namespace Alpha.MemberInfo.Criteria.Transfer
{
    public class UpdateCashBalanceTransferCriteria : CriteriaBase
    {
        public UpdateCashBalanceTransferCriteria(
            int custId,
            decimal amount,
            int userId,
            int subAccId,
            string ip)
        {
            CustId = custId;
            Amount = amount;
            UserId = userId;
            SubAccId = subAccId;
            IP = ip;
            ErrCode = 0;
            ErrMsg = string.Empty;
        }

        public int CustId { get; }

        public decimal Amount { get; }

        public int UserId { get; }

        public int SubAccId { get; }

        public string IP { get; set; }

        public int ErrCode { get; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey() => "";

        public override bool IsValid() => CustId > 0 && Amount != 0 && UserId > 0 && !string.IsNullOrWhiteSpace(IP);
    }
}
