namespace Alpha.MemberInfo.Site.Features.Shared
{
    using Alpha.MemberInfo.Features.Shared;
    using Alpha.MemberInfo.Features.Shared.Models;
    using Alpha.MemberInfo.Site.Features.Shared.Requests;
    using MediatR;
    using System.Collections.Generic;

    public class UserNameSuggestionHandler :
        IRequestHandler<UserNameSuggestionQuery, IEnumerable<UsernameSuggestion>>
    {
        private readonly UsernameSuggestionService usernameSuggestionService;

        public UserNameSuggestionHandler(UsernameSuggestionService usernameSuggestionService)
        {
            this.usernameSuggestionService = usernameSuggestionService;
        }

        public IEnumerable<UsernameSuggestion> Handle(UserNameSuggestionQuery message)
            => usernameSuggestionService.GetUsernamesSuggestion(
                message.CustId,
                message.SuggestionQuery,
                message.IsInternal,
                message.DirectDownlines,
                message.IsAccInfo);
    }
}