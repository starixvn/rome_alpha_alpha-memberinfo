using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.Data.Repository;

namespace Alpha.MemberInfo.Criteria.Transfer
{
    public abstract class AddCashBalanceTransferCriteria : NonQueryCommand
    {
        protected AddCashBalanceTransferCriteria(
            int uplineId,
            int custId,
            decimal amount,
            string superIdsTransferTotal,
            int subAccId,
            string ip,
            string languageKey)
            : this(uplineId, custId.ToString(), amount, true, false, string.Empty, superIdsTransferTotal, uplineId, subAccId, ip, languageKey)
        {
        }

        protected AddCashBalanceTransferCriteria(
            int fromCustId,
            string toCustIds,
            decimal amount,
            bool isCashTransfer,
            bool isFullTransfer,
            string custStatus,
            string superIdsTransferTotal,
            int creator,
            int subAccId,
            string ip,
            string languageKey)
        {
            FromCustId = fromCustId;
            ToCustIds = toCustIds;
            Amount = amount;
            IsCashTransfer = isCashTransfer;
            IsFullTransfer = isFullTransfer;
            CustStatus = custStatus;
            SuperIdsTransferTotal = superIdsTransferTotal;
            Creator = creator;
            SubAccId = subAccId;
            IP = ip;
            Language = languageKey;
            ErrCode = 0;
            ErrMsg = string.Empty;
        }

        public int FromCustId { get; }

        public string ToCustIds { get; }

        public decimal Amount { get; }

        public bool IsCashTransfer { get; set; }

        public bool IsFullTransfer { get; set; }

        public string CustStatus { get; set; }

        public string SuperIdsTransferTotal { get; }

        public int Creator { get; }

        public int SubAccId { get; }

        public string IP { get; set; }

        public string Language { get; set; }

        public string ErrMsg { get; set; }

        public int ErrCode { get; set; }

        public override bool IsValid() => FromCustId > 0 &&
            !string.IsNullOrWhiteSpace(ToCustIds) &&
            Creator > 0 &&
            !string.IsNullOrEmpty(IP);
    }
}
