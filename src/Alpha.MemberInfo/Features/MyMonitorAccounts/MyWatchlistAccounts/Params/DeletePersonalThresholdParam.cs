namespace Alpha.MemberInfo.Features.MyMonitorAccounts.MyWatchlistAccounts.Params
{
    public class DeletePersonalThresholdParam
    {
        public DeletePersonalThresholdParam(
           int custId,
           string username,
           int userId,
           int subAccId,
           string ip)
        {
            CustId = custId;
            Username = username;
            UserId = userId;
            SubAccId = subAccId;
            IP = ip;
        }

        public int CustId { get; }

        public string Username { get; }

        public int UserId { get; }

        public int SubAccId { get; }

        public string IP { get; }
    }
}
