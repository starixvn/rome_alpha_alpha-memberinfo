namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests
{
    using Alpha.MemberInfo.Models;
    using MediatR;

    public class CheckExistedCustomerRequest : QRCodeBaseQuery, IAsyncRequest<bool>
    {
        public CheckExistedCustomerRequest(IUser viewer) : base(viewer)
        {
        }

        public string Username { get; set; }

        public override bool IsValid() => 
            base.IsValid() && 
            !string.IsNullOrEmpty(Username);
    }
}