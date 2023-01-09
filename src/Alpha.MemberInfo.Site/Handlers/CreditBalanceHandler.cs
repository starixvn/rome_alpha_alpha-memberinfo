namespace Alpha.MemberInfo.Site.Handlers
{
    using System.Threading.Tasks;
    using Alpha.MemberInfo.Services;
    using MediatR;
    using Params;
    using Requests.CreditBalance;
    using Services.CreditBalance;
    using Services.Transfer;
    using ViewModels.CreditBalance;

    public class CreditBalanceHandler
        : IAsyncRequestHandler<CreditBalanceQuery, CreditBalanceViewModel>
    {
        private readonly CreditBalanceService creditBalanceService;
        private readonly TransferService transferService;
        private readonly IWinlossService winlossService;

        public CreditBalanceHandler(
            CreditBalanceService creditBalanceService,
            TransferService transferService,
            IWinlossService winlossService)
        {
            this.creditBalanceService = creditBalanceService;
            this.transferService = transferService;
            this.winlossService = winlossService;
        }

        public async Task<CreditBalanceViewModel> Handle(CreditBalanceQuery message)
        {
            var param = new CreditBalanceParam(
                message.CustId,
                message.CustLevel,
                message.UserName,
                message.StatusId.Value,
                message.PageIndex,
                message.PageSize,
                message.IsAdminCanViewVNIP);

            var pagedCreditBalances = await creditBalanceService.GetPagedCreditBalancesAsync(param);

            var transferCondition = message.ActorLevel.IsAgentLevel()
                ? await transferService.GetAllowTransfer(message.ActorId)
                : null;

            var reportEntered = winlossService.GetEnteredSportResults();

            return new CreditBalanceViewModel(pagedCreditBalances, transferCondition, message, reportEntered);
        }
    }
}