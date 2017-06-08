using Alpha.MemberInfo.Models;
using MediatR;

namespace Alpha.MemberInfo.Site.Requests.Transfer
{
    public class CashBalanceTransferCommand : IAsyncRequest<Result>
    {
        public int CustId { get; set; }

        public decimal Amount { get; set; }

        public string SuperIdsTransferTotal { get; set; }

        public int UplineId { get; set; }

        public int SubAccId { get; set; }

        public string IP { get; set; }

        public string LanguageKey { get; set; }

        public bool IsValid() => CustId > 0 && Amount != 0;
    }
}