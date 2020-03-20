namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests
{
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Models;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Requests;
    using MediatR;
    using System.Collections.Generic;

    public class UserNameSuggestionQuery : MemberInfoBaseQuery, IRequest<IEnumerable<UsernameSuggestion>>
    {
        public UserNameSuggestionQuery(IUser viewer) : base(viewer)
        {
            this.ActorId = viewer.Id;
            this.IsInternal = viewer.IsInternal;
        }

        public string SuggestionQuery { get; set; }

        public int ActorId { get; }

        public bool IsInternal { get; }

    public override bool IsValid() => !string.IsNullOrEmpty(SuggestionQuery);
    }
}