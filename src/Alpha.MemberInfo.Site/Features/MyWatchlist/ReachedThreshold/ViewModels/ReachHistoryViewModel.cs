namespace Alpha.MemberInfo.Site.Features.MyWatchlist.ReachedThreshold.ViewModels
{
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Models;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.ReachedThreshold.Requests;
    using System.Collections.Generic;

    public class ReachHistoryViewModel
    {
        public ReachHistoryViewModel(ReachHistoryQuery message, IEnumerable<ReachHistoryDTO> reachHistoryDTOs)
        {
            ReachHistories = reachHistoryDTOs;
            ReachHistoryQuery = message;
        }

        public IEnumerable<ReachHistoryDTO> ReachHistories { get; }

        public ReachHistoryQuery ReachHistoryQuery { get; }
    }
}