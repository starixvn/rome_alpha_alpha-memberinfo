namespace Alpha.MemberInfo.Site.Features.ParlayMaxPayout
{
    using Alpha.MemberInfo.Features.ParlayMaxPayout;
    using Alpha.MemberInfo.Features.ParlayMaxPayout.Params;
    using Alpha.MemberInfo.Models;
    using MediatR;
    using Requests;
    using System.Collections.Generic;
    using System.Linq;
    using ViewModels;

    public class ParlayMaxPayoutHandler :
        IRequestHandler<ParlayMaxPayoutSingleSelectQuery, ParlayMaxPayoutSinglePageViewModel>,
        IRequestHandler<ParlayMaxPayoutMultipleSelectQuery, ParlayMaxPayoutMultiplePageViewModel>,
        IRequestHandler<ParlayMaxPayoutUpdateSingleQuery, Result>,
        IRequestHandler<ParlayMaxPayoutUpdateMultipleQuery, IEnumerable<Result>>
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

        public Result Handle(ParlayMaxPayoutUpdateSingleQuery message)
            => parlayMaxPayoutService.UpdateParlayMaxPayout(
                new ParlayMaxPayoutUpdateParam(
                     message.CustId,
                     message.MaxPayoutLimit,
                     message.UserId,
                     message.Language,
                     message.Ip));

        public IEnumerable<Result> Handle(ParlayMaxPayoutUpdateMultipleQuery message)
        {
            var custIds = message.CustIdList.Split(',').Select(int.Parse);
            foreach (var custId in custIds)
            {
                yield return parlayMaxPayoutService.UpdateParlayMaxPayout(
                    new ParlayMaxPayoutUpdateParam(
                     custId,
                     message.MaxPayoutLimit,
                     message.UserId,
                     message.Language,
                     message.Ip));
            }
        }
    }
}