namespace Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.Requests
{
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Requests;
    using MediatR;
    using System.Collections.Generic;

    public class PersonalPresetQuery : MemberInfoBaseQuery, IAsyncRequest<IEnumerable<PersonalPresetModel>>
    {
        public PersonalPresetQuery(IUser viewer) : base(viewer)
        {
        }
    }
}