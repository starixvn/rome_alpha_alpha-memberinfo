namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.MyWatchlistAccounts.Requests
{
    using Alpha.MemberInfo.Features.MyMonitorAccounts.MyWatchlistAccounts.Models;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Requests;
    using MediatR;
    using System.Collections.Generic;

    public class AddPersonalThresholdCommand : MemberInfoBaseQuery, IAsyncRequest<IEnumerable<NamedResult>>
    {
        public AddPersonalThresholdCommand(IUser viewer) : base(viewer)
        {
        }

        public string Usernames { get; set; }

        public decimal? WinningThreshold { get; set; }

        public decimal? LosingThreshold { get; set; }
    }
}