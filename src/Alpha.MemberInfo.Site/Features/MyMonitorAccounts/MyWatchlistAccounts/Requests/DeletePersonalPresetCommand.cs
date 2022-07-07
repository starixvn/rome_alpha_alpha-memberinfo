namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.MyWatchlistAccounts.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Requests;
    using MediatR;

    public class DeletePersonalPresetCommand : MemberInfoBaseQuery, IAsyncRequest<Result>
    {
        public DeletePersonalPresetCommand(IUser viewer) : base(viewer)
        {
        }

        public int ID { get; set; }
    }
}