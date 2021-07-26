namespace Alpha.MemberInfo.Site.Handlers
{
    using System.Threading.Tasks;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Params.IndirectLoginAccount;
    using Alpha.MemberInfo.Services.IndirectLoginAccount;
    using Alpha.MemberInfo.Site.Helpers;
    using Alpha.MemberInfo.Site.Requests.IndirectLoginAccount;
    using Alpha.MemberInfo.Site.ViewModels.IndirectLoginAccount;
    using Alpha.Utilities.IP;
    using Alpha.Web.Helpers;
    using MediatR;
    using Sunplus.Agent.Utilities;

    public class IndirectLoginAccountHandler :
        IAsyncRequestHandler<ManageAccountQuery, ManageAccountViewModel>,
        IRequestHandler<NewAccountQuery, object>,
        IAsyncRequestHandler<ValidateAccountQuery, Result>,
        IAsyncRequestHandler<NewAccountCommand, Result>,
        IRequestHandler<UpdatePasswordQuery, UpdatePasswordViewModel>,
        IAsyncRequestHandler<UpdatePasswordCommand, Result>,
        IAsyncRequestHandler<DisconnectCommand, Result>,
        IAsyncRequestHandler<RedirectQuery, string>
    {
        private const string HashKeySessionKey = "Alpha.MemberInfo.Site.Handlers.IndirectLoginAccountHandler.PasswordHash";

        private readonly IndirectLoginAccountService service;
        private readonly SessionManager sessionManager;
        private readonly Encryptor encryptor;
        private readonly IPService ipService;

        public IndirectLoginAccountHandler(
            IndirectLoginAccountService service,
            SessionManager sessionManager,
            Encryptor encryptor,
            IPService ipService)
        {
            this.service = service;
            this.sessionManager = sessionManager;
            this.encryptor = encryptor;
            this.ipService = ipService;
        }

        public async Task<ManageAccountViewModel> Handle(ManageAccountQuery message)
            => new ManageAccountViewModel(await service.GetIndirectLoginAccountsAsync(message.ViewerId, message.ViewerSubAccId));

        public object Handle(NewAccountQuery message) => GetHashKey();

        public async Task<Result> Handle(ValidateAccountQuery message)
            => await service.ValidateIndirectLoginAccountAsync(message.ViewerId, message.ViewerSubAccId, message.CustId, message.SubAccId);

        public async Task<Result> Handle(NewAccountCommand message)
        {
            var countryCode = GetCountryCode(message.IP);
            var requestInfo = RequestInfoHelper.GetRequestInfo(message.Request, message.IP, message.BrowserSize, countryCode);

            return await service.NewAccountAsync(new NewAccountParam(
                message.CustName,
                message.Password,
                sessionManager.Get<string>(HashKeySessionKey),
                message.ViewerId,
                message.ViewerSubAccId,
                message.ViewerLevelId,
                message.LanguageKey,
                requestInfo));
        }


        public UpdatePasswordViewModel Handle(UpdatePasswordQuery message)
            => new UpdatePasswordViewModel(message.CustId, message.SubAccId, message.CustName, GetHashKey());

        public async Task<Result> Handle(UpdatePasswordCommand message)
        {
            var countryCode = GetCountryCode(message.IP);
            var requestInfo = RequestInfoHelper.GetRequestInfo(message.Request, message.IP, message.BrowserSize, countryCode);

            return await service.UpdatePasswordAsync(new UpdatePasswordParam(
                message.CustId,
                message.SubAccId,
                message.CustName,
                message.CustLevelId,
                message.Password,
                sessionManager.Get<string>(HashKeySessionKey),
                message.ViewerId,
                message.ViewerSubAccId,
                message.ViewerUsername,
                message.LanguageKey,
                requestInfo));
        }


        public async Task<Result> Handle(DisconnectCommand message)
            => await service.DisconnectAsync(new DisconnectAccountParam(
                message.CustId,
                message.SubAccId,
                message.CustName,
                message.ViewerId,
                message.ViewerSubAccId,
                message.ViewerUsername,
                message.LanguageKey,
                message.IP));

        public async Task<string> Handle(RedirectQuery message)
            => await service.BuildRedirectURLAsync(new RedirectParam(
                message.CustId,
                message.SubAccId,
                message.Language,
                message.ViewerId,
                message.ViewerSubAccId,
                message.ViewerUsername,
                message.BrowserSize,
                message.IBCPKey,
                message.CustName,
                message.DarkModeName));

        private string GetHashKey()
        {
            var hashKey = encryptor.RandomString(4);
            sessionManager.Add(HashKeySessionKey, hashKey);

            return hashKey;
        }

        private string GetCountryCode(string ip)
        {
            var countryCode = string.Empty;
            var ipInformation = ipService.GetIPInformation(ip);
            if (ipInformation != null)
            {
                countryCode = ipInformation.CountryLong == "Reserved" ? null : ipInformation.CountryLong;
            }

            return countryCode;
        }
    }
}