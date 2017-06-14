using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.MemberInfo.Criteria.Transfer
{
    public class AddAgentCashBalanceTransferCriteria : AddCashBalanceTransferCriteria
    {
        public AddAgentCashBalanceTransferCriteria(
            int uplineId,
            int custId,
            decimal amount,
            string superIdsTransferTotal,
            int subAccId,
            string ip,
            string languageKey)
            : base(uplineId, custId.ToString(), amount, true, false, string.Empty, superIdsTransferTotal, uplineId, subAccId, ip, languageKey)
        {
        }

        public override string GetSettingKey()
        {
            return "Age_Transfer_Ins_Agent";
        }
    }
}
