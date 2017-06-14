using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.MemberInfo.Criteria.Transfer
{
    public class AddMemberCashBalanceTransferCriteria : AddCashBalanceTransferCriteria
    {
        public AddMemberCashBalanceTransferCriteria(
            int uplineId,
            int custId,
            decimal amount,
            string superIdsTransferTotal,
            int subAccId,
            string ip,
            string languageKey,
            bool isAllDayBalTransfer)
            : base(uplineId, custId.ToString(), amount, true, false, string.Empty, superIdsTransferTotal, uplineId, subAccId, ip, languageKey)
        {
            IsAllDayBalTransfer = isAllDayBalTransfer;
        }

        public bool IsAllDayBalTransfer { get; set; }

        public override string GetSettingKey()
        {
            return "Age_Transfer_Ins_Member";
        }

        public override bool IsValid()
        {
            return base.IsValid() && IsAllDayBalTransfer;
        }
    }
}
