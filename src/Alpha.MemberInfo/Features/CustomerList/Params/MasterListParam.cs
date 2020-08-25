namespace Alpha.MemberInfo.Site.Features.CustomerList.Params
{
    public class MasterListParam : CustomerListParamBase
    {
        public MasterListParam(
            int srecommend,
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
            Srecommend = srecommend;
        }

        public int Srecommend { get; }
    }
}