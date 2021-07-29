namespace Alpha.MemberInfo.Site.Features.ParlayMaxPayout
{
    using Alpha.MemberInfo.Features.ParlayMaxPayout;
    using Alpha.MemberInfo.Features.ParlayMaxPayout.Params;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Params;
    using Alpha.MemberInfo.Services;
    using Constants;
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
        private readonly ParlayMaxPayoutService parlayMaxPayoutService;
        private readonly CheckUMService checkUMService;

        public ParlayMaxPayoutHandler(
            ParlayMaxPayoutService parlayMaxPayoutService,
            CheckUMService checkUMService)
        {
            this.parlayMaxPayoutService = parlayMaxPayoutService;
            this.checkUMService = checkUMService;
        }

        public ParlayMaxPayoutSinglePageViewModel Handle(ParlayMaxPayoutSingleSelectQuery message)
            => new ParlayMaxPayoutSinglePageViewModel(
                message,
                parlayMaxPayoutService.GetParlayMaxPayout(new ParlayMaxPayoutSelectParam(message.CustId)));

        public ParlayMaxPayoutMultiplePageViewModel Handle(ParlayMaxPayoutMultipleSelectQuery message)
            => new ParlayMaxPayoutMultiplePageViewModel(
                message,
                parlayMaxPayoutService.GetParlayMaxPayout(new ParlayMaxPayoutSelectParam(message.SelectedCustId)));

        public Result Handle(ParlayMaxPayoutUpdateSingleQuery message)
        {
            var checkUMResult = checkUMService.CheckUM(
                new CheckUMParam(message.SelectedCustId, NonDcsProduct.Sportsbook, message.Language));

            return checkUMResult.IsFailed
                ? checkUMResult
                : parlayMaxPayoutService.UpdateParlayMaxPayout(
                    new ParlayMaxPayoutUpdateParam(
                        message.SelectedCustId,
                        message.MaxPayoutLimit,
                        message.UserId,
                        message.SubAccId,
                        message.Language,
                        message.IP));
        }

        public IEnumerable<Result> Handle(ParlayMaxPayoutUpdateMultipleQuery message)
        {
            var custIds = message.CustIdList.Split(',').Select(int.Parse);
            foreach (var custId in custIds)
            {
                var checkUMResult = checkUMService.CheckUM(
                    new CheckUMParam(custId, NonDcsProduct.Sportsbook, message.Language));

                yield return checkUMResult.IsFailed
                    ? checkUMResult
                    : parlayMaxPayoutService.UpdateParlayMaxPayout(
                        new ParlayMaxPayoutUpdateParam(
                            custId,
                            message.MaxPayoutLimit,
                            message.UserId,
                            message.SubAccId,
                            message.Language,
                            message.IP));
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