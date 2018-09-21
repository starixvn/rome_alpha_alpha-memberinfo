namespace Alpha.MemberInfo.Site.Handlers
{
    using System.Threading.Tasks;
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

        public CreditBalanceHandler(
            CreditBalanceService creditBalanceService,
            TransferService transferService)
        {
            this.creditBalanceService = creditBalanceService;
            this.transferService = transferService;
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

            return new CreditBalanceViewModel(pagedCreditBalances, transferCondition, message);
        }
    }
}