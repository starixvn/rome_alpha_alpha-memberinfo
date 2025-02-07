namespace Alpha.MemberInfo.Site.Features.Shared.Requests
{
    using Alpha.MemberInfo.Features.Shared.Models;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Requests;
    using MediatR;
    using System.Collections.Generic;

    public class UserNameSuggestionQuery : MemberInfoBaseQuery, IRequest<IEnumerable<UsernameSuggestion>>
    {
        public UserNameSuggestionQuery(IUser viewer) : base(viewer)
        {
            DirectDownlines = false;
        }

        public string SuggestionQuery { get; set; }

        public bool DirectDownlines { get; set; }

        public bool IsAccInfo { get; set; } = true;

        public bool ExcludeMember { get; set; }

        public bool IsAgent => ActorLevel.IsAgentLevel();

        public override bool IsValid() => !string.IsNullOrEmpty(SuggestionQuery) && base.IsValid();
    }
}