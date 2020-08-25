namespace Alpha.MemberInfo.Site.Features.CustomerList.Params
{
    public class MemberListParam : CustomerListParamBase
    {
        public MemberListParam(
            int userId,
            int recommend,
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
            Recommend = recommend;
        }

        public int UserId { get; }

        public int Recommend { get; }
    }
}