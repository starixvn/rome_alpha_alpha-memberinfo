namespace Alpha.MemberInfo.Site.Features.CustomerList.Params
{
    public class AgentListParam : CustomerListParamBase
    {
        public AgentListParam(
            int userId,
            int mrecommend,
            string username,
            bool? enabled,
            bool? closed,
            bool? suspended,
            byte doubleCommLosing,
            byte productUnSync,
            int pageSize,
            int pageIndex,
            bool limitedDateToShowInfo)
            :  base(username,
                    enabled,
                    closed,
                    suspended,
                    doubleCommLosing,
                    productUnSync,
                    pageSize,
                    pageIndex,
                    limitedDateToShowInfo)
        {
            UserId = userId;
            Mrecommend = mrecommend;
        }

        public int UserId { get; }

        public int Mrecommend { get; }
    }
}