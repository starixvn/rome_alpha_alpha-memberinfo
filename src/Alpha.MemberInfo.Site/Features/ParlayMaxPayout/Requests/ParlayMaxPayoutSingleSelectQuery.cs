namespace Alpha.MemberInfo.Site.Features.ParlayMaxPayout.Requests
{
    using MediatR;
    using ViewModels;

    public class ParlayMaxPayoutSingleSelectQuery : IRequest<ParlayMaxPayoutSinglePageViewModel>
    {
        public int CustId { get; set; }

        public string CustName { get; set; }
    }
}