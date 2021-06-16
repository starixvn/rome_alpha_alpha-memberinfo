namespace Alpha.MemberInfo.Site.Features.ParlayMaxPayout.Requests
{
    using Alpha.MemberInfo.Models;
    using MediatR;

    public class ParlayMaxPayoutCheckDownlineQuery : IRequest<Result>
    {
        public ParlayMaxPayoutCheckDownlineQuery(IUser viewer)
        {
            Language = viewer.Language;
        }

        public string CustIdList { get; set; }

        public string MaxPayoutLimit { get; set; }

        public string Language { get; }
    }
}