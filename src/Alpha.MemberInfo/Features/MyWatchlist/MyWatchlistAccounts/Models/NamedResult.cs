namespace Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models
{
    using Alpha.MemberInfo.Models;

    public class NamedResult : Result
    {
        public NamedResult()
        {
        }

        public NamedResult(string name, int code, string message)
        {
            Name = name;
            Code = code;
            Message = message;
        }

        public string Name { get; set; }
    }
}
