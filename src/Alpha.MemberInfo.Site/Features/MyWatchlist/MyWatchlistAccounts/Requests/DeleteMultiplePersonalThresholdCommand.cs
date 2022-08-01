namespace Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.Requests
{
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Requests;
    using MediatR;
    using System.Collections.Generic;

    public class DeleteMultiplePersonalThresholdCommand : MemberInfoBaseQuery, IAsyncRequest<IEnumerable<NamedResult>>
    {
        public DeleteMultiplePersonalThresholdCommand(IUser viewer) : base(viewer)
        {
        }

        public UserInfo[] Users { get; set; }

        public override bool IsValid()
            => base.IsValid() && !IsInternal;
    }
}