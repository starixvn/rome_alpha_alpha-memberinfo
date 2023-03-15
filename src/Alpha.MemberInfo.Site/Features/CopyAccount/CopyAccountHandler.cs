using Alpha.MemberInfo.AppResources;
using Alpha.MemberInfo.Enumerations;
using Alpha.MemberInfo.Features.QRCodeMemberSignIn;
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
        IAsyncRequestHandler<CheckUsernameQuery, bool>,
        IAsyncRequestHandler<CheckExistedCustomerQuery, bool>
    {
        private const string HashKeySessionKey = "Alpha.MemberInfo.Site.Features.CopyAccount.CopyAccountHandler.PasswordHash";

        private readonly Encryptor encryptor;
        private readonly IAesService aes;
        private readonly SessionManager sessionManager;
        private readonly QRCodeMemberSignInService qrCodeMemberSignInService;
        private readonly ICopyAccountService copyAccountService;

        public CopyAccountHandler(
            Encryptor encryptor,
            IAesService aes,
            SessionManager sessionManager,
            QRCodeMemberSignInService qrCodeMemberSignInService,
            ICopyAccountService copyAccountService)
        {
            this.encryptor = encryptor;
            this.aes = aes;
            this.sessionManager = sessionManager;
            this.qrCodeMemberSignInService = qrCodeMemberSignInService;
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
                qrCodeMemberSignInService.GetSuggestUsername(message.ViewerUsername, message.ViewerId, message.ViewerLevelId),
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
            var model = qrCodeMemberSignInService.CheckBelongingByUsername(message.CustId, message.SourceUsername, message.SubAccId);
            var sourceCustId = model.CustId;

            if (sourceCustId > 0 && qrCodeMemberSignInService.IsEnableCustomer(sourceCustId))
            {
                message.Password = aes.Decrypt(message.Password, sessionManager.Get<string>(HashKeySessionKey));
                return await copyAccountService.CreateAccount(message, sourceCustId);
            }

            return new CopyAccountResponse
            {
                Success = false,
                Code = 100,
                Message = Resources.UsernameIsInvalid
            };
        }

        public Task<CreditInfo> Handle(CreditQuery message)
            => copyAccountService.GetCreditInfo(message.CustName);

        public Task<bool> Handle(CheckUsernameQuery message)
        {
            var result = qrCodeMemberSignInService.CheckBelongingByUsername(message.ViewerId, message.CustName, message.SubAccId);
            var isValid = result.CustId > 0 && Enumeration.FromValue<UserLevel>((byte)result.RoleId).IsDirectDownlineOf(message.ActorLevel);
            return Task.FromResult(isValid);
        }

        public Task<bool> Handle(CheckExistedCustomerQuery message)
        {
            return Task.FromResult(qrCodeMemberSignInService.CheckExistedCustomer(message.CustName));
        }
    }
}