namespace Alpha.MemberInfo.Features.DormantAccountList.Criteria
{
    using Alpha.MemberInfo.Constants;
    using Alpha.MemberInfo.Features.DormantAccountList.Params;
    using Alpha.MemberInfo.Models;
    using Fanex.Data.Repository;

    public class GetDormantAccountsCriteria : CriteriaBase
    {
        public GetDormantAccountsCriteria(DormantAccountParam param)
        {
            CustId = param.CustId;
            FilterUsername = param.Username;
            FilterRoleId = param.RoleId;
            FilterStatusId = param.StatusId;
            PageIndex = param.PageIndex;
            PageSize = param.PageSize;
        }

        public int CustId { get; }

        public string FilterUsername { get; }

        public byte FilterRoleId { get; }

        public byte FilterStatusId { get; }

        public int PageIndex { get; }

        public int PageSize { get; }

        public int TotalItems { get; set; }

        public override string GetSettingKey()
            => "Acc_DormantAccountList_Sel";

        public override bool IsValid()
            => CustId > 0
            && FilterRoleId < UserLevel.Super.Value
            && (FilterStatusId == StatusIdentity.All || FilterStatusId == StatusIdentity.Closed || FilterStatusId == StatusIdentity.Disabled);
    }
}
