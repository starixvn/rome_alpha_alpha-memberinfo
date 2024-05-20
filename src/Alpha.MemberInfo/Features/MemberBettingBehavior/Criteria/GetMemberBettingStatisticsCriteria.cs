namespace Alpha.MemberInfo.Features.MemberBettingBehavior.Criteria
{
    using Fanex.Data.Repository;
    using System;

    public class GetMemberBettingStatisticsCriteria : CriteriaBase
    {
        public GetMemberBettingStatisticsCriteria(int custId, DateTime dateStart, DateTime dateEnd)
        {
            CustId = custId;
            DateStart = dateStart;
            DateEnd = dateEnd;
        }

        public int CustId { get; }

        public DateTime DateStart { get; }

        public DateTime DateEnd { get; }

        public override string GetSettingKey()
            => "Acc_Rpt_CustomerBettingBehavior_Statistic_Sel";

        public override bool IsValid()
            => CustId > 0;
    }
}
