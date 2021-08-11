namespace Alpha.MemberInfo.Site.Features.ParlayMaxPayout.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Requests;
    using MediatR;
    using ViewModels;

    public class ParlayMaxPayoutSingleSelectQuery : MemberInfoBaseQuery, IRequest<ParlayMaxPayoutSinglePageViewModel>
    {
        public ParlayMaxPayoutSingleSelectQuery(IUser viewer) : base(viewer)
        {
        }
    }
}