
namespace Alpha.MemberInfo.Features.MemberBettingBehavior.Criteria
{
    using Fanex.Data.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GetCustomerProfileCriteria : CriteriaBase
    {
        public GetCustomerProfileCriteria(int custId)
        {
            this.CustId = custId;
        }

        public int CustId { get; }


        public override string GetSettingKey()
            => "Age_CustomerBehaviorProfile_Sel";

        public override bool IsValid()
            => CustId > 0;
    }
}
