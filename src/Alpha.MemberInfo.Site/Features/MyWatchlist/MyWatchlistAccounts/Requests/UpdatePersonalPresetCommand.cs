namespace Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Requests;
    using MediatR;
    using System.Web.Mvc;

    public class UpdatePersonalPresetCommand : MemberInfoBaseQuery, IAsyncRequest<Result>
    {
        public UpdatePersonalPresetCommand(IUser viewer) : base(viewer)
        {
        }

        public int ID { get; set; }

        [AllowHtml]
        public string PresetTitle { get; set; }

        public decimal WinningThreshold { get; set; }

        public decimal LosingThreshold { get; set; }

        public override bool IsValid()
            => base.IsValid() && !IsInternal;
    }
}