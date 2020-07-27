namespace Alpha.MemberInfo.Features.Shared
{
    using Alpha.MemberInfo.Features.Shared.Criteria;
    using Alpha.MemberInfo.Features.Shared.Models;
    using Fanex.Data.Repository;
    using System.Collections.Generic;
    using System.Linq;

    public interface UsernameSuggestionService
    {
        IEnumerable<UsernameSuggestion> GetUsernamesSuggestion(
            int actorId, 
            string suggestionQuery, 
            bool isInternal,
            bool directDownline);
    }

    public class UsernameSuggestionServiceImpl : UsernameSuggestionService
    {
        private readonly IDynamicRepository dynamicRepository;

        public UsernameSuggestionServiceImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public IEnumerable<UsernameSuggestion> GetUsernamesSuggestion(
            int actorId, 
            string suggestionQuery, 
            bool isInternal, 
            bool directDownline)
            => dynamicRepository.Fetch<UsernameSuggestion>(
              new SelectAutoSuggestionCriteria(actorId, suggestionQuery, isInternal, directDownline)).OrderBy(item => item.UserName);
    }
}
