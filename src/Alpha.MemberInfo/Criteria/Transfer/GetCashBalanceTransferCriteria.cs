using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.Data.Repository;

namespace Alpha.MemberInfo.Criteria.Transfer
{
    public class GetCashBalanceTransferCriteria : CriteriaBase
    {
        public GetCashBalanceTransferCriteria(int custId)
        {
            CustId = custId;
        }

        public int CustId { get; }

        public override string GetSettingKey() => "Age_Transfer_CashBalance_Sel";

        public override bool IsValid() => CustId > 0;
    }
}
