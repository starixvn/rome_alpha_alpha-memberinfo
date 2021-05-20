namespace Alpha.MemberInfo.Site.Features.ParlayMaxPayout
{
    using Alpha.MemberInfo.Features.ParlayMaxPayout;
    using Alpha.MemberInfo.Features.ParlayMaxPayout.Params;
    using MediatR;
    using Requests;
    using ViewModels;

    public class ParlayMaxPayoutHandler :
        IRequestHandler<ParlayMaxPayoutSingleSelectQuery, ParlayMaxPayoutSinglePageViewModel>,
        IRequestHandler<ParlayMaxPayoutMultipleSelectQuery, ParlayMaxPayoutMultiplePageViewModel>
    {
        private readonly ParlayMaxPayoutService parlayMaxPayoutService;

        public ParlayMaxPayoutHandler(ParlayMaxPayoutService parlayMaxPayoutService)
        {
            this.parlayMaxPayoutService = parlayMaxPayoutService;
        }

        public ParlayMaxPayoutSinglePageViewModel Handle(ParlayMaxPayoutSingleSelectQuery message)
            => new ParlayMaxPayoutSinglePageViewModel(
                message,
                parlayMaxPayoutService.GetParlayMaxPayout(new ParlayMaxPayoutSelectParam(message.CustId)));

        public ParlayMaxPayoutMultiplePageViewModel Handle(ParlayMaxPayoutMultipleSelectQuery message)
            => new ParlayMaxPayoutMultiplePageViewModel(
                message,
                parlayMaxPayoutService.GetParlayMaxPayout(new ParlayMaxPayoutSelectParam(message.CustId)));
    }
}