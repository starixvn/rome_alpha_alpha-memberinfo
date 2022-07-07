namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.MyWatchlistAccounts.Requests
{
    using Alpha.MemberInfo.Features.MyMonitorAccounts.MyWatchlistAccounts.Models;
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
    }
}