namespace Alpha.MemberInfo.Features.MyMonitorAccounts.MyWatchlistAccounts.Models
{
    public class NamedResult
    {
        public NamedResult()
        {
        }

        public NamedResult(string name, int errCode, string errMsg)
        {
            Name = name;
            ErrCode = errCode;
            ErrMsg = errMsg;
        }

        public string Name { get; }

        public int ErrCode { get; }

        public string ErrMsg { get; }
    }
}
