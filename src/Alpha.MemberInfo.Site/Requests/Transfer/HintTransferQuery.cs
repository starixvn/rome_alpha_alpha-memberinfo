using Alpha.MemberInfo.Models;
using Alpha.MemberInfo.Site.ViewModels.Transfer;
using MediatR;

namespace Alpha.MemberInfo.Site.Requests.Transfer
{
    public class HintTransferQuery : IAsyncRequest<HintTransferViewModel>
    {
        public HintTransferQuery(IUser user)
        {
            CustId = user.Id;
            Language = user.Language;
        }

        public int CustId { get; }

        public string Language { get; }

        public bool ShowWarningTransfer { get; set; }

        public bool IsValid() => CustId > 0;
    }
}