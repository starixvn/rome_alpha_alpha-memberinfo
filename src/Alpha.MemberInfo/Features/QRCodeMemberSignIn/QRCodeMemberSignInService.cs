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

        SportBookSettingModel GetExistedMemberSetting(int existedCustId);

        ResultCreateMemberQRCode CreateMemberByQRCode(InsertMemberCommand command);
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

        public SportBookSettingModel GetExistedMemberSetting(int existedCustId)
            => dynamicRepository.Get<SportBookSettingModel>(new GetExistedMemberSettingCriteria(existedCustId));

        public ResultCreateMemberQRCode CreateMemberByQRCode(InsertMemberCommand command)
        {
            dynamicRepository.Execute(command);

            return new ResultCreateMemberQRCode{
               Success = command.Success,
               CustId = command.Custid,
               ErrCode = command.ErrCode,
               ErrMsg = command.ErrMsg
            };
        }
    }
}
