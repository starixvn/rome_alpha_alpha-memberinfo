namespace Alpha.MemberInfo.Site.Requests.Transfer
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.ViewModels.Transfer;
    using MediatR;

    public class HintTransferQuery : IAsyncRequest<HintTransferViewModel>
    {
        public HintTransferQuery(IUser user)
        {
            CustId = user.Id;
            RoleId = user.LevelId;
            Language = user.Language;
        }

        public int CustId { get; }

        public byte RoleId { get; }

        public string Language { get; }

        public bool ShowWarningTransfer { get; set; }

        public bool IsValid() => CustId > 0;
    }
}