namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests
{
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Models;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Requests;
    using MediatR;
    using System.Collections.Generic;

    public class UserNameSuggestionQuery : MemberInfoBaseQuery, IRequest<IEnumerable<UsernameSuggestion>>
    {
        private readonly IUser viewer;

        public UserNameSuggestionQuery(IUser viewer) : base(viewer)
        {
            this.viewer = viewer;
        }

        public string SuggestionQuery { get; set; }

        public int ActorId => viewer.Id;

        public bool IsInternal => true; // viewer.IsInternal;

        public override bool IsValid() => true; //viewer.ViewMemberInfo && IsValidName(SuggestionQuery);
    }
}