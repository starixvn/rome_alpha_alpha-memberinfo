namespace Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Params
{
    public class SelectMultipleWatchlistParam
    {
        public SelectMultipleWatchlistParam(string custList, int agentId, string siteName)
        {
            CustList = custList;
            AgentId = agentId;
            SiteName = siteName;
        }

        public string CustList { get; }
        public int AgentId { get; }

        public string SiteName { get; }
    }
}
