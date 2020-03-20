namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests
{
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Models;
    using Alpha.MemberInfo.Models;
    using MediatR;
    using System.Collections.Generic;

    public class UserNameSuggestionQuery : QRCodeBaseQuery, IRequest<IEnumerable<UsernameSuggestion>>
    {
        public UserNameSuggestionQuery(IUser viewer) : base(viewer)
        {
            this.ActorId = viewer.Id;
            this.IsInternal = viewer.IsInternal;
        }

        public string SuggestionQuery { get; set; }

        public int ActorId { get; }

        public bool IsInternal { get; }

        public override bool IsValid() => base.IsValid() && !string.IsNullOrEmpty(SuggestionQuery);
    }
}