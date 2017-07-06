using Alpha.MemberInfo.Site.ViewModels.Transfer;
using MediatR;

namespace Alpha.MemberInfo.Site.Requests.Transfer
{
    public class CashBalanceTransferQuery : IAsyncRequest<CashBalanceTransferViewModel>
    {
        public int CustId { get; set; }

        public bool IsValid() => CustId > 0;
    }
}