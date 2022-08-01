namespace Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Models
{
    using System.Collections.Generic;

    public class ReachlistAccountDTO
    {
        public ReachlistAccountDTO(IEnumerable<ReachlistAccountModel> reachlistAccounts, int totalItems)
        {
            ReachlistAccounts = reachlistAccounts;
            TotalItems = totalItems;
        }

        public IEnumerable<ReachlistAccountModel> ReachlistAccounts { get; }

        public int TotalItems { get; }
    }
}
