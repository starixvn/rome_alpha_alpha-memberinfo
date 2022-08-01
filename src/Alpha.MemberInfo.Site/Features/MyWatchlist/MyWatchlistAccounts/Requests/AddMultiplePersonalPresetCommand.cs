namespace Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.Requests
{
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Requests;
    using MediatR;
    using System.Collections.Generic;

    public class AddMultiplePersonalPresetCommand : MemberInfoBaseQuery, IAsyncRequest<IEnumerable<NamedResult>>
    {
        public AddMultiplePersonalPresetCommand(IUser viewer) : base(viewer)
        {
        }

        public PresetInfo[] Presets { get; set; }

        public override bool IsValid()
            => base.IsValid() && !IsInternal;
    }
}