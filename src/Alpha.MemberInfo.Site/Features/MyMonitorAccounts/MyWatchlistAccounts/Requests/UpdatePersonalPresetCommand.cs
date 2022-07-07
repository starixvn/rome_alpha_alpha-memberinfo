namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.MyWatchlistAccounts.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Requests;
    using MediatR;

    public class UpdatePersonalPresetCommand : MemberInfoBaseQuery, IAsyncRequest<Result>
    {
        public UpdatePersonalPresetCommand(IUser viewer) : base(viewer)
        {
        }

        public int ID { get; set; }

        public string PresetTitle { get; set; }

        public decimal WinningThreshold { get; set; }

        public decimal LosingThreshold { get; set; }
    }
}