using Alpha.MemberInfo.Site.ViewModels.Transfer;
using MediatR;

namespace Alpha.MemberInfo.Site.Requests.Transfer
{
    public class CashBalanceTransferQuery : IRequest<CashBalanceTransferViewModel>
    {
        public int CustId { get; set; }

        public string CustName { get; set; }

        public bool IsValid() => CustId > 0;
    }
}