namespace Alpha.MemberInfo.Services.IndirectLoginAccount
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Configurations;
    using Alpha.MemberInfo.Criteria.IndirectLoginAccount;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Models.IndirectLoginAccount;
    using Alpha.MemberInfo.Params.IndirectLoginAccount;
    using Fanex.Data.Repository;
    using RSACryptoPad;
    using Sunplus.Agent.Utilities;

    public interface IndirectLoginAccountService
    {
        Task<IEnumerable<IndirectLoginAccountItem>> GetIndirectLoginAccountsAsync(int custId, int subAccId);

        Task<Result> ValidateIndirectLoginAccountAsync(int custId, int subAccId, int indirectCustId, int indirectSubAccId);

        Task<Result> NewAccountAsync(NewAccountParam param);

        Task<Result> UpdatePasswordAsync(UpdatePasswordParam param);

        Task<Result> DisconnectAsync(DisconnectAccountParam param);

        Task<string> BuildRedirectURLAsync(RedirectParam param);
    }

    public class IndirectLoginAccountServiceImpl : IndirectLoginAccountService
    {
        private const int NeedToReEnterPassword = 5001;
        private const int KeySize = 1024;

        private readonly IDynamicRepository dynamicRepository;
        private readonly AesService aes;
        private readonly Encryptor encryptor;

        public IndirectLoginAccountServiceImpl(
            IDynamicRepository dynamicRepository,
            AesService aes,
            Encryptor encryptor)
        {
            this.dynamicRepository = dynamicRepository;
            this.aes = aes;
            this.encryptor = encryptor;
        }

        public async Task<IEnumerable<IndirectLoginAccountItem>> GetIndirectLoginAccountsAsync(int custId, int subAccId)
            => await dynamicRepository.FetchAsync<IndirectLoginAccountItem>(new GetIndirectLoginAccountsCriteria(custId, subAccId));

        public async Task<Result> ValidateIndirectLoginAccountAsync(int custId, int subAccId, int indirectCustId, int indirectSubAccId)
        {
            var criteria = new GetIndirectLoginAccountsCriteria(custId, subAccId, indirectCustId, indirectSubAccId);
            var account = await dynamicRepository.GetAsync<IndirectLoginAccountItem>(criteria);

            if (account == null || !account.Enabled || account.Closed)
            {
                return Result.CreateErrorResult(Resources.IndirectLoginAccountClosedOrDisabledError);
            }

            if (account.LastVerifyPwd.HasValue
                && account.LastChangedPwdDate.HasValue
                && account.LastVerifyPwd <= account.LastChangedPwdDate)
            {
                return Result.CreateErrorResult(string.Empty, NeedToReEnterPassword);
            }

            return new Result();
        }

        public async Task<Result> NewAccountAsync(NewAccountParam param)
        {
            param.Password = DecryptAndEncodePassword(param.Password, param.HashKey);

            var criteria = new NewAccountCriteria(param);
            await dynamicRepository.ExecuteAsync(criteria);

            var result = new Result { Code = criteria.ErrCode, Message = criteria.ErrMsg };

            if (result.IsOk)
            {
                result.Message = Resources.IndirectLoginAccountNewSuccessfully;
            }

            return result;
        }

        public async Task<Result> UpdatePasswordAsync(UpdatePasswordParam param)
        {
            param.Password = DecryptAndEncodePassword(param.Password, param.HashKey);

            var criteria = new UpdatePasswordCriteria(param);
            await dynamicRepository.ExecuteAsync(criteria);

            return new Result
            {
                Code = criteria.ErrCode,
                Message = criteria.ErrMsg
            };
        }

        private string DecryptAndEncodePassword(string encryptPassword, string hashKey)
        {
            var rawPassword = aes.Decrypt(encryptPassword, hashKey);

            return encryptor.CfsEnCode(rawPassword);
        }

        public async Task<Result> DisconnectAsync(DisconnectAccountParam param)
        {
            var criteria = new DisconnectAccountCriteria(param);
            await dynamicRepository.ExecuteAsync(criteria);

            var result = new Result { Code = criteria.ErrCode, Message = criteria.ErrMsg };

            if (result.IsOk)
            {
                result.Message = Resources.IndirectLoginAccountDisconnectSuccessfully;
            }

            return result;
        }

        public async Task<string> BuildRedirectURLAsync(RedirectParam param)
        {
            // IndirectCustId, IndirectSubAccId, LoginTimeTicks, Language, CustId, SubAccId, Username, BrowserSize
            var parameter = string.Format(
                "{0}&{1}&{2}&{3}&{4}&{5}&{6}&{7}",
                param.IndirectCustId,
                param.IndirectSubAccId,
                DateTime.Now,
                param.Language,
                param.ActorId,
                param.ActorSubAccId,
                param.ActorUsername,
                param.BrowserSize);
            parameter = new EncryptionThread().EncryptString(parameter, KeySize, param.IBCPKey);

            return $"{AppSettings.IndirectLoginURL}?params={parameter}";
        }
    }
}
