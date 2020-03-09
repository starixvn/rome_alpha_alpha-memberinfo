namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.ViewModels;
    using Alpha.MemberInfo.Site.Requests;
    using MediatR;

    public class GetMemberCreditRequest : MemberInfoBaseQuery, IRequest<MemberCreditRange>
    {
        private readonly IUser viewer;

        public GetMemberCreditRequest(IUser viewer) : base(viewer)
        {
            this.viewer = viewer;
        }

        public string SuggestionQuery { get; set; }

        public int ActorId => viewer.Id;

        public bool IsInternal => true; // viewer.IsInternal;

        public override bool IsValid() => true; //viewer.ViewMemberInfo && IsValidName(SuggestionQuery);
    }
}