namespace Alpha.MemberInfo.Site.Features.ParlayMaxPayout
{
    using Alpha.MemberInfo.AppResources;
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
        IRequestHandler<ParlayMaxPayoutUpdateMultipleQuery, IEnumerable<Result>>,
        IRequestHandler<ParlayMaxPayoutCheckDownlineQuery, Result>
    {
        private const int NotAuthorizedErrorCode = 401;
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
        {
            if (!message.CanUpdateAccount)
            {
                return Result.CreateErrorResult(Lang.NotAuthorized, NotAuthorizedErrorCode);
            }

            return parlayMaxPayoutService.UpdateParlayMaxPayout(
                           new ParlayMaxPayoutUpdateParam(
                                message.CustId,
                                message.MaxPayoutLimit,
                                message.UserId,
                                message.SubAccId,
                                message.Language,
                                message.Ip));
        }

        public IEnumerable<Result> Handle(ParlayMaxPayoutUpdateMultipleQuery message)
        {
            if (!message.CanUpdateAccount)
            {
                yield return Result.CreateErrorResult(Lang.NotAuthorized, NotAuthorizedErrorCode);
                yield break;
            }

            var custIds = message.CustIdList.Split(',').Select(int.Parse);
            foreach (var custId in custIds)
            {
                yield return parlayMaxPayoutService.UpdateParlayMaxPayout(
                    new ParlayMaxPayoutUpdateParam(
                     custId,
                     message.MaxPayoutLimit,
                     message.UserId,
                     message.SubAccId,
                     message.Language,
                     message.Ip));
            }
        }

        public Result Handle(ParlayMaxPayoutCheckDownlineQuery message)
        {
            var custId = message.CustIdList.Split(',').Select(int.Parse).FirstOrDefault();

            return parlayMaxPayoutService.CheckDownline(
                new ParlayMaxPayoutCheckDownlineParam(
                    custId,
                    message.MaxPayoutLimit,
                    message.Language));
        }
    }
}