namespace Alpha.MemberInfo.Features.QRCodeMemberSignIn
{
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Criteria;
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Models;
    using Fanex.Data.Repository;
    using System.Collections.Generic;

    public interface QRCodeMemberSignInService
    {
        IEnumerable<UsernameSuggestion> GetUsernamesSuggestion(int actorId, string suggestionQuery, bool isInternal);

        CreditRangeModel GetCreditRange(string SearchAccountName);
    }

    public class QRCodeMemberSignInServiceImpl : QRCodeMemberSignInService
    {
        private readonly IDynamicRepository dynamicRepository;

        public QRCodeMemberSignInServiceImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public IEnumerable<UsernameSuggestion> GetUsernamesSuggestion(int actorId, string suggestionQuery, bool isInternal)
            => dynamicRepository.Fetch<UsernameSuggestion>(
              new SelectAutoSuggestionCriteria(actorId, suggestionQuery, isInternal));

        public CreditRangeModel GetCreditRange(string SearchAccountName)
            => dynamicRepository.Get<CreditRangeModel>(new SelectCreditRangeCriteria(SearchAccountName));
    }
}
