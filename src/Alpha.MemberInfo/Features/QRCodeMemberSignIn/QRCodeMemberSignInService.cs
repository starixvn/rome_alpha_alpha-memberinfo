namespace Alpha.MemberInfo.Features.QRCodeMemberSignIn
{
    using Alpha.CopyAccount.Params;
    using Alpha.CopyAccount.Services;
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Criteria;
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Models;
    using Alpha.MemberInfo.Models;
    using Fanex.Data.Repository;
    using System.Collections.Generic;
    using System.Linq;

    public interface QRCodeMemberSignInService
    {
        IEnumerable<UsernameSuggestion> GetUsernamesSuggestion(int actorId, string suggestionQuery, bool isInternal);

        CreditAndStatusModel GetCreditAndStatus(string SearchAccountName);

        ResultCreateMemberQRCode CreateMemberByQRCode(InsertMemberCommand command);

        CheckBelongingByUsernameModel CheckBelongingByUsername(int actorId, string username, int subAccountId);

        bool IsEnableCustomer(int custId);

        string GetSuggestUsername(string prefix, int custId);

        SelectRnGStatusModel GetRnGStatus(string username);

        bool IsNotFirstLoginByQRCode(int custId);

        Result ResetMemberPassword(ResetMainAccountPasswordCommand command);

        bool CheckExistedCustomer(string username);
    }

    public class QRCodeMemberSignInServiceImpl : QRCodeMemberSignInService
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly ICopyAccountService copyAccountService;
        private const byte AlphaSite = 2;

        public QRCodeMemberSignInServiceImpl(IDynamicRepository dynamicRepository, ICopyAccountService copyAccountService)
        {
            this.dynamicRepository = dynamicRepository;
            this.copyAccountService = copyAccountService;
        }

        public IEnumerable<UsernameSuggestion> GetUsernamesSuggestion(int actorId, string suggestionQuery, bool isInternal)
            => dynamicRepository.Fetch<UsernameSuggestion>(
              new SelectAutoSuggestionCriteria(actorId, suggestionQuery, isInternal)).OrderBy(item => item.UserName);

        public CreditAndStatusModel GetCreditAndStatus(string SearchAccountName)
            => dynamicRepository.Get<CreditAndStatusModel>(new SelectCreditAndStatusCriteria(SearchAccountName));

        public ResultCreateMemberQRCode CreateMemberByQRCode(InsertMemberCommand command)
        {
            var result = copyAccountService.Copy(new CopyAccountParams(
                command.ActorId,
                UserLevel.Agent.Value,
                command.SubAccId,
                command.SubUsername,
                command.Username,
                command.Password,
                command.Phone,
                command.MobilePhone,
                command.Fax,
                command.Credit,
                command.CreditMax,
                command.CopyFromCustId,
                command.CopyFromUsername,
                command.Language,
                command.IP,
                AlphaSite,
                isQrCode: true));

            if (result != null)
            {
                return new ResultCreateMemberQRCode
                {
                    CustId = result.CustId,
                    ErrCode = result.Code,
                    ErrMsg = result.Message,
                    RawPassword = result.Password,
                    Success = result.IsSuccess,
                    CustIdCopyFrom = command.CopyFromCustId
                };
            }

            return new ResultCreateMemberQRCode();
        }

        public CheckBelongingByUsernameModel CheckBelongingByUsername(int actorId, string username, int subAccountId)
        {
            var criteria = new CheckBelongingByUsernameCritieria(actorId, username, subAccountId);
            dynamicRepository.Execute(criteria);
            return new CheckBelongingByUsernameModel(criteria.IsActive, criteria.CustId, criteria.IsSubAcc, criteria.RoleId);
        }

        public bool IsEnableCustomer(int custId)
        {
            var criteria = new CheckEnableCustomerCritieria(custId);
            dynamicRepository.Execute(criteria);
            return criteria.ErrCode.Equals(0);
        }

        public string GetSuggestUsername(string prefix, int custId)
        {
            var criteria = new SelectSuggestUsernameCriteria(prefix, custId);
            dynamicRepository.Execute(criteria);
            return criteria.SeqName;
        }

        public SelectRnGStatusModel GetRnGStatus(string username)
            => dynamicRepository.Get<SelectRnGStatusModel>(new SelectRnGStatusCriteria(username));

        public Result ResetMemberPassword(ResetMainAccountPasswordCommand command)
        {
            dynamicRepository.Execute(command);
            return Result.CreateErrorResult(command.ErrMsg, command.ErrCode);
        }

        public bool IsNotFirstLoginByQRCode(int custId)
          => dynamicRepository.Get<bool>(new SelectFirstLoginByQRCodeCriteria(custId));

        public bool CheckExistedCustomer(string username)
        {
            var criteria = new CheckExistedCustomerCritieria(username);
            dynamicRepository.Execute(criteria);
            return criteria.IsExists;
        }
    }
}
