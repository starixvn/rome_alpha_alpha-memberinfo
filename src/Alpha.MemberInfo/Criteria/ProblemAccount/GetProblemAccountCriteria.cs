namespace Alpha.MemberInfo.Criteria.ProblemAccount
{
    using System;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Params.ProblemAccount;
    using Fanex.Data.Repository;

    public class GetProblemAccountCriteria : CriteriaBase
    {
        public GetProblemAccountCriteria(GetProblemAccountParam param)
        {
            CustId = param.CustId;
            SubAccId = param.SubAccId;
            UplineUserName = param.UplineUserName;
            SearchRoleId = param.SearchRoleId;
            SearchUserName = param.SearchUserName;
            FromDate = param.FromDate;
            ToDate = param.ToDate;
            ReviewStatusId = param.ReviewStatusId;
        }

        public int CustId { get; }

        public int SubAccId { get; }

        public string UplineUserName { get; }

        public byte SearchRoleId { get; }

        public string SearchUserName { get; }

        public int ReviewStatusId { get; }

        public DateTime FromDate { get; }

        public DateTime ToDate { get; }

        public override string GetSettingKey()
            => "Age_ProblemAccount_CustomerList";

        public override bool IsValid()
            => CustId > 0 &&
            SubAccId >= 0 &&
            SearchRoleId <= UserLevel.Master.Value;            
    }
}
