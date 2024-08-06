namespace Alpha.MemberInfo.Site.Features.Shared.Requests
{
    using Alpha.MemberInfo.Features.Shared.Models;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Requests;
    using MediatR;
    using System.Collections.Generic;

    public class UserNameSuggestionByRoleQuery : MemberInfoBaseQuery, IRequest<IEnumerable<UsernameSuggestion>>
    {
        public UserNameSuggestionByRoleQuery(IUser viewer) : base(viewer)
        {
        }

        public string SuggestionQuery { get; set; }

        public byte RoleId { get; set; }

        public override bool IsValid() =>
            base.IsValid() &&
            !string.IsNullOrEmpty(SuggestionQuery);
    }
}