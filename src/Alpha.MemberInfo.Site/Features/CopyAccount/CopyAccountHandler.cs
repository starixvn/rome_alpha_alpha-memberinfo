using Alpha.MemberInfo.AppResources;
using Alpha.MemberInfo.Enumerations;
using Alpha.MemberInfo.Models;
using Alpha.MemberInfo.Site.Features.CopyAccount.Request;
using Alpha.MemberInfo.Site.Features.CopyAccount.ViewModels;
using Alpha.Web.Helpers;
using Fanex.Cryptography.Aes;
using Fanex.Customer.Client.Models;
using Fanex.Customer.Client.Params;
using Fanex.Customer.Client.Services;
using MediatR;
using Sunplus.Agent.Utilities;
using System;
using System.Threading.Tasks;

namespace Alpha.MemberInfo.Site.Features.CopyAccount
{
    public class CopyAccountHandler :
        IAsyncRequestHandler<CopyAccountQuery, CopyAccountViewModel>,
        IAsyncRequestHandler<CopyAccountCommand, CopyMultipleCustomersResult>,
        IAsyncRequestHandler<CreditQuery, CreditInfo>,
        IAsyncRequestHandler<CheckUsernameQuery, MemberInfo.Models.Result>,
        IAsyncRequestHandler<CheckExistedCustomerQuery, bool>
    {
        private const string HashKeySessionKey = "Alpha.MemberInfo.Site.Features.CopyAccount.CopyAccountHandler.PasswordHash";

        private readonly Encryptor encryptor;
        private readonly IAesService aes;
        private readonly SessionManager sessionManager;
        private readonly ICustomerService customerService;

        public CopyAccountHandler(
            Encryptor encryptor,
            IAesService aes,
            SessionManager sessionManager,
            ICustomerService customerService)
        {
            this.encryptor = encryptor;
            this.aes = aes;
            this.sessionManager = sessionManager;
            this.customerService = customerService;
        }

        public async Task<CopyAccountViewModel> Handle(CopyAccountQuery message)
        {
            var limitNewDownlineResult = await customerService.CheckAllLimitedRules(
                   message.ViewerId,
                   message.ViewerLevelId,
                   message.CustName,
                   message.Language);

            if (limitNewDownlineResult.IsFailed)
            {
                return new CopyAccountViewModel(true, limitNewDownlineResult.Message, message);
            }

            return new CopyAccountViewModel(
                message,
                await customerService.GetSuggestUsername(message.CustName, message.ViewerLevelId),
                GetHasKey());
        }

        private string GetHasKey()
        {
            var hashKey = encryptor.RandomString(4);

            sessionManager.Add(HashKeySessionKey, hashKey);

            return hashKey;
        }

        public async Task<CopyMultipleCustomersResult> Handle(CopyAccountCommand message)
        {
            var model = await customerService.GetDownlineByUsername(message.CustId, message.SubAccId, message.SourceUsername);
            var sourceCustId = model.CustId;
            message.Password = aes.Decrypt(message.Password, sessionManager.Get<string>(HashKeySessionKey));
            if (string.IsNullOrWhiteSpace(message.Password) || sourceCustId <= 0 || !Enumeration.FromValue<UserLevel>(model.RoleId).IsDirectDownlineOf(message.ActorLevel))
            {
                return new CopyMultipleCustomersResult
                {
                    Code = 1,
                    Message = Lang.NotAuthorized
                };
            }

            message.Password = encryptor.CfsEnCode(message.Password);

            return await customerService.CopyMultipleCustomers(new CopyMultipleCustomersParam(
                message.ViewerId,
                message.SubAccId,
                message.CustId,
                message.CustLevel.GetDirectDownLineLevel().Value,
                message.ActorUsername,
                message.Username,
                message.Password,
                message.Credit,
                message.MemberMaxCredit,
                sourceCustId,
                message.NumberOfAccounts,
                message.Site,
                false,
                message.IP,
                message.CustDBGroupId,
                message.Language));
        }

        public async Task<CreditInfo> Handle(CreditQuery message)
        {
            var result = await customerService.GetDownlineByUsername(message.ViewerId, message.SubAccId, message.CustName);
            if (result.CustId > 0 && Enumeration.FromValue<UserLevel>(result.RoleId).IsDirectDownlineOf(message.ActorLevel))
            {
                return await customerService.GetCreditInfo(message.CustName);
            }
            else
            {
                throw new InvalidOperationException(Lang.NotAuthorized);
            }
        }

        public async Task<MemberInfo.Models.Result> Handle(CheckUsernameQuery message)
        {
            var result = await customerService.GetDownlineByUsername(message.ViewerId, message.SubAccId, message.CustName);
            if (result.CustId > 0 && Enumeration.FromValue<UserLevel>(result.RoleId).IsDirectDownlineOf(message.ActorLevel))
            {
                if (await customerService.IsCustomerEnabled(result.CustId))
                {
                    return MemberInfo.Models.Result.CreateSuccessResult();
                }
                else
                {
                    return MemberInfo.Models.Result.CreateErrorResult(Resources.AccountWasDisabled);
                }
            }

            return MemberInfo.Models.Result.CreateErrorResult(Resources.UserNotExist);
        }

        public Task<bool> Handle(CheckExistedCustomerQuery message)
        {
            return customerService.CheckExistedCustomer(message.CustName);
        }
    }
}