namespace Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models
{
    public class ManageWatchlistAccountsModel
    {
        public int CustId { get; set; }

        public string Username { get; set; }

        public decimal? WinningThreshold { get; set; }

        public decimal? LosingThreshold { get; set; }
    }
}
