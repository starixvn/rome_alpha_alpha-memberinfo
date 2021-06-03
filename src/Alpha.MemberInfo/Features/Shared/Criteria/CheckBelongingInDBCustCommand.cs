namespace Alpha.MemberInfo.Features.Shared.Criteria
{
    using Alpha.MemberInfo.Features.Shared.Params;
    using Fanex.Data.Repository;

    public class CheckBelongingInDBCustCommand : NonQueryCommand
    {
        public CheckBelongingInDBCustCommand(CheckBelongingParam param)
        {
            SessionCustId = param.ViewerId;
            CustId = param.CustId;
            SubAccountId = param.SubAccId;
            TransId = param.TransId;
        }

        public int SessionCustId { get; }

        public int CustId { get; }

        public int SubAccountId { get; }

        public long TransId { get; }

        public bool IsActive { get; set; }

        public string Username { get; set; }

        public byte RoleId { get; set; }

        public override string GetSettingKey()
            => "Age_CustomerSelCheckBelonging_DBCUST";

        public override bool IsValid()
            => SessionCustId > 0
            && (CustId > 0 || TransId > 0)
            && SubAccountId >= 0;
    }
}
