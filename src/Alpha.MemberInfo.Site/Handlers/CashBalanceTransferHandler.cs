using System.Threading.Tasks;
using Alpha.MemberInfo.Enumerations;
using Alpha.MemberInfo.Models;
using Alpha.MemberInfo.Services;
using Alpha.MemberInfo.Site.Requests.Transfer;
using Alpha.MemberInfo.Site.ViewModels.Transfer;
using MediatR;

namespace Alpha.MemberInfo.Site.Handlers
{
    public class CashBalanceTransferHandler :
        IAsyncRequestHandler<CashBalanceTransferQuery, CashBalanceTransferViewModel>,
        IAsyncRequestHandler<CashBalanceTransferCommand, Result>,
        IAsyncRequestHandler<HintTransferQuery, HintTransferViewModel>
    {
        private readonly CashBalanceTransferService service;

        public CashBalanceTransferHandler(CashBalanceTransferService service)
        {
            this.service = service;
        }

        public async Task<CashBalanceTransferViewModel> Handle(CashBalanceTransferQuery message)
        {
            var model = await service.GetCashBalanceTransferAsync(message.CustId);

            return new CashBalanceTransferViewModel(
                model.UplineCashBalance,
                model.CustId,
                model.UserName,
                Enumeration.FromValue<UserLevel>(model.RoleId),
                model.CashBalance,
                model.Outstanding,
                model.GivenCredit);
        }

        public async Task<Result> Handle(CashBalanceTransferCommand command)
        {
            return await service.AddCashBalanceTransferAsync(
                command.UplineId,
                command.CustId,
                command.Amount,
                command.UplineId,
                command.SubAccId,
                command.IP,
                command.LanguageKey);
        }

        public async Task<HintTransferViewModel> Handle(HintTransferQuery message)
        {
            string allowTransfer = string.Empty;
            var userRole = Enumeration.FromValue<UserLevel>(message.RoleId);

            if (userRole.IsNotSuperLevel())
            {
                allowTransfer = await service.GetAllowTransfer(message.CustId);
            }

            return new HintTransferViewModel(allowTransfer, message.ShowWarningTransfer);
        }
    }
}