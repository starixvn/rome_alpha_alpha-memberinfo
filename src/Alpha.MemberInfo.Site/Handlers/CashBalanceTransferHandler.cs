using Alpha.MemberInfo.Enumerations;
using Alpha.MemberInfo.Models;
using Alpha.MemberInfo.Site.Requests.Transfer;
using Alpha.MemberInfo.Site.ViewModels.Transfer;
using MediatR;

namespace Alpha.MemberInfo.Site.Handlers
{
    public class CashBalanceTransferHandler :
        IRequestHandler<CashBalanceTransferQuery, CashBalanceTransferViewModel>
    {
        public CashBalanceTransferViewModel Handle(CashBalanceTransferQuery message)
        {
            byte uplineUserLevelValue = 4;
            var viewModel = new CashBalanceTransferViewModel(
                248417.24M,
                "logan_master",
                197074.9M,
                2489.02M,
                59500,
                Enumeration.FromValue<UserLevel>(uplineUserLevelValue));

            return viewModel;
        }
    }
}