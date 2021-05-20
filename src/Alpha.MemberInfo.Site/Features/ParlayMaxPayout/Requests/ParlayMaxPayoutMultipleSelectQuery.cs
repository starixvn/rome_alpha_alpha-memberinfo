namespace Alpha.MemberInfo.Site.Features.ParlayMaxPayout.Requests
{
    using Fanex.Common;
    using MediatR;
    using ViewModels;

    public class ParlayMaxPayoutMultipleSelectQuery : IRequest<ParlayMaxPayoutMultiplePageViewModel>
    {
        public string CustIds { get; set; }

        public string CustNames { get; set; }

        public int CustId
            => Converter.ToInt(CustIds.Split(',')[0]);
    }
}