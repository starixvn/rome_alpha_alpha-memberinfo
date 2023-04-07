using Alpha.MemberInfo.AppResources;
using Alpha.MemberInfo.Enumerations;
using Alpha.MemberInfo.Models;
using Alpha.MemberInfo.Site.Features.CopyAccount.Models;
using Alpha.MemberInfo.Site.Features.CopyAccount.Request;
using Alpha.MemberInfo.Site.Features.CopyAccount.ViewModels;
using Alpha.Web.Helpers;
using Fanex.Cryptography.Aes;
using MediatR;
using Sunplus.Agent.Utilities;
using System.Threading.Tasks;

namespace Alpha.MemberInfo.Site.Features.CopyAccount
{
    public class CopyAccountHandler :
        IAsyncRequestHandler<CopyAccountQuery, CopyAccountViewModel>,
        IAsyncRequestHandler<CopyAccountCommand, CopyAccountResponse>,
        IAsyncRequestHandler<CreditQuery, CreditInfo>,
        IAsyncRequestHandler<CheckUsernameQuery, Result>,
        IAsyncRequestHandler<CheckExistedCustomerQuery, bool>
    {
        private const string HashKeySessionKey = "Alpha.MemberInfo.Site.Features.CopyAccount.CopyAccountHandler.PasswordHash";

        private readonly Encryptor encryptor;
        private readonly IAesService aes;
        private readonly SessionManager sessionManager;
        private readonly ICopyAccountService copyAccountService;

        public CopyAccountHandler(
            Encryptor encryptor,
            IAesService aes,
            SessionManager sessionManager,
            ICopyAccountService copyAccountService)
        {
            this.encryptor = encryptor;
            this.aes = aes;
            this.sessionManager = sessionManager;
            this.copyAccountService = copyAccountService;
        }

        public async Task<CopyAccountViewModel> Handle(CopyAccountQuery message)
        {
            var limitNewDownlineResult = await copyAccountService.CheckLimitNewDownline(
                   message.ViewerId,
                   message.ViewerLevelId,
                   message.Language);

            if (limitNewDownlineResult.IsFailed)
            {
                return new CopyAccountViewModel(true, limitNewDownlineResult.Message);
            }

            return new CopyAccountViewModel(
                message,
                await copyAccountService.GetSuggestUsername(message.ViewerUsername, message.ViewerId, message.ViewerLevelId),
                GetHasKey());
        }

        private string GetHasKey()
        {
            var hashKey = encryptor.RandomString(4);

            sessionManager.Add(HashKeySessionKey, hashKey);

            return hashKey;
        }

        public async Task<CopyAccountResponse> Handle(CopyAccountCommand message)
        {
            var model = await copyAccountService.GetDownlineByUsername(message.CustId, message.SubAccId, message.SourceUsername);
            var sourceCustId = model.CustId;
            message.Password = aes.Decrypt(message.Password, sessionManager.Get<string>(HashKeySessionKey));
            if (string.IsNullOrWhiteSpace(message.Password))
            {
                return new CopyAccountResponse
                {
                    Code = 1,
                    Message = Lang.NotAuthorized
                };
            }

            message.Password = encryptor.CfsEnCode(message.Password);

            return await copyAccountService.CreateAccount(message, sourceCustId);
        }

        public Task<CreditInfo> Handle(CreditQuery message)
            => copyAccountService.GetCreditInfo(message.CustName);

        public async Task<Result> Handle(CheckUsernameQuery message)
        {
            var result = await copyAccountService.GetDownlineByUsername(message.ViewerId, message.SubAccId, message.CustName);
            if(result.CustId > 0 && Enumeration.FromValue<UserLevel>(result.RoleId).IsDirectDownlineOf(message.ActorLevel))
            {
                if (await copyAccountService.IsCustomerEnabled(result.CustId))
                {
                    return Result.CreateSuccessResult();
                }
                else
                {
                    return Result.CreateErrorResult(Resources.AccountWasDisabled);
                }
            }

            return Result.CreateErrorResult(Resources.UserNotExist);
        }

        public Task<bool> Handle(CheckExistedCustomerQuery message)
        {
            return copyAccountService.CheckExistedCustomer(message.CustName);
        }
    }
}