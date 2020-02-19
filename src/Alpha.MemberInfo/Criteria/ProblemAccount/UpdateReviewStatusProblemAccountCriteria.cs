namespace Alpha.MemberInfo.Criteria.ProblemAccount
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Params.ProblemAccount;
    using Fanex.Data.Repository;

    public class UpdateReviewStatusProblemAccountCriteria : NonQueryCommand
    {
        public UpdateReviewStatusProblemAccountCriteria(UpdateProblemAccountParam param)
        {
            UserId = param.CustId;
            SubAccId = param.SubAccId;
            RoleId = param.RoleId;
            CustomerList = param.CustomerList;
            IsAffectedAllDownlines = param.IsAffectedAllDownlines;
            Language = param.Language;
            LoginIp = param.LoginIp;
        }

        public int UserId { get; }

        public int SubAccId { get; }

        public byte RoleId { get; }

        public string CustomerList { get; }

        public bool IsAffectedAllDownlines { get; }

        public string Language { get; }

        public string LoginIp { get; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey()
            => "Age_ProblemAccount_ReviewStatusUpd";
        public override bool IsValid()
            => UserId > 0 &&
            SubAccId >= 0 &&
            RoleId <= UserLevel.Master.Value &&
            !string.IsNullOrEmpty(CustomerList);
    }
}
