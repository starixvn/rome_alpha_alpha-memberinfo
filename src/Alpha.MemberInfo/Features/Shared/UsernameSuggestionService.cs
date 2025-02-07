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
            bool directDownline,
            bool isAccInfo,
            bool excludeMember);

        IEnumerable<UsernameSuggestion> GetUsernameSuggestionByRole(int custId, string username, byte roleId);
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
            bool directDownline,
            bool isAccInfo,
            bool excludeMember)
        {
            var criteria = new SelectAutoSuggestionCriteria(
                actorId,
                suggestionQuery,
                isInternal,
                directDownline,
                isAccInfo,
                excludeMember);

            return dynamicRepository.Fetch<UsernameSuggestion>(criteria).OrderBy(item => item.UserName);
        }

        public IEnumerable<UsernameSuggestion> GetUsernameSuggestionByRole(int custId, string username, byte roleId)
            => dynamicRepository.Fetch<UsernameSuggestion>(new UsernameSuggestionByRoleCriteria(custId, username, roleId));
    }
}