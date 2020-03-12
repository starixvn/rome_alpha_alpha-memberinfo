namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.ViewModels;
    using Alpha.MemberInfo.Site.Requests;
    using MediatR;

    public class ShowQRCodeRequest :
        MemberInfoBaseQuery,
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

        public override bool IsValid() => true;
    }
}