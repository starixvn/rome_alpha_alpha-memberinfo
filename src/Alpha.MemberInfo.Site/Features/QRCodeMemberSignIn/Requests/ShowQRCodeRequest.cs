namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.ViewModels;
    using MediatR;

    public class ShowQRCodeRequest :
        QRCodeBaseQuery,
        IRequest<ShowQRCodeViewModel>
    {
        public ShowQRCodeRequest(IUser viewer) : base(viewer)
        {
        }

        public int SearchAccountId { get; set; }

        public string SearchAccountName { get; set; }

        public string FirstName { get; set; }

        public decimal Credit { get; set; }

        public decimal CreditMin { get; set; }

        public decimal CreditMax { get; set; }

        public string MemberNameCopyFrom { get; set; }

        public bool ShowMessage { get; set; }

        public bool ShowPageTitle { get; set; } = true;

        public override bool IsValid() => 
            base.IsValid()
            && SearchAccountId > 0 
            && !string.IsNullOrEmpty(SearchAccountName)
            && Credit <= CreditMax
            && Credit >= CreditMin;
    }
}