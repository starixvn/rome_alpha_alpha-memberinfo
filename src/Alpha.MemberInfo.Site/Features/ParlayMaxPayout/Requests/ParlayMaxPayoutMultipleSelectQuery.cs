namespace Alpha.MemberInfo.Site.Features.ParlayMaxPayout.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Requests;
    using Fanex.Common;
    using MediatR;
    using ViewModels;

    public class ParlayMaxPayoutMultipleSelectQuery : MemberInfoBaseQuery, IRequest<ParlayMaxPayoutMultiplePageViewModel>
    {
        public ParlayMaxPayoutMultipleSelectQuery(IUser viewer) : base(viewer)
        {
        }

        public string CustIds { get; set; }

        public string CustNames { get; set; }

        public int SelectedCustId
            => Converter.ToInt(CustIds.Split(',')[0]);

        public override bool IsValid()
            => base.IsValid()
            && !string.IsNullOrWhiteSpace(CustIds)
            && !string.IsNullOrWhiteSpace(CustNames);
    }
}