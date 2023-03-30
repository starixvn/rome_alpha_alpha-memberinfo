using Alpha.MemberInfo.Models;
using Alpha.MemberInfo.Site.Features.CopyAccount.Models;
using Alpha.MemberInfo.Site.Features.CopyAccount.Request;
using Alpha.MemberInfo.Site.Features.CopyAccount.ViewModels;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.MemberInfo.Site.Features.CopyAccount
{
    public interface ICopyAccountService
    {
        Task<CopyAccountResponse> CreateAccount(CopyAccountCommand command, int sourceCustId);

        Task<CreditInfo> GetCreditInfo(string username);

        Task<Result> CheckLimitNewDownline(int custId, byte custLevel, string language);

        Task<string> GetSuggestUsername(string actorName, int custId, byte roleId);

        Task<bool> CheckExistedCustomer(string username);

        Task<Downline> GetDownlineByUsername(int custId, int subAccountId, string username);
    }

    public class CopyAccountService : ICopyAccountService
    {
        private readonly HttpClient httpClient;

        public CopyAccountService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<CopyAccountResponse> CreateAccount(CopyAccountCommand command, int sourceCustId)
        {
            var payload = new
            {
                actorId = command.ViewerId,
                actorSubAccId = command.SubAccId,
                actorSubAccName = command.SubUsername,
                actorUserLevel = command.ViewerLevelId,
                userName = command.Username,
                password = command.Password,
                credit = command.Credit,
                maxCredit = command.MemberMaxCredit,
                sourceCustId,
                sourceUsername = command.SourceUsername,
                numberOfAccounts = command.NumberOfAccounts,
                language = command.Language,
                ip = command.IP,
                systemId = 2
            };

            return await PostAsJsonAsync<CopyAccountResponse>("api/customer/copy", payload);
        }

        public Task<CreditInfo> GetCreditInfo(string username)
            => GetAsJsonAsync<CreditInfo>($"api/customer/{username}/credit");

        private async Task<T> PostAsJsonAsync<T>(string requestUri, object payload = null)
        {
            using (var stringContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json"))
            {
                using (var request = new HttpRequestMessage(HttpMethod.Post, requestUri) { Content = stringContent })
                {
                    using (var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false))
                    {
                        response.EnsureSuccessStatusCode();
                        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                    }
                }
            }
        }

        private async Task<T> GetAsJsonAsync<T>(string requestUri)
        {
            using (var response = await httpClient.GetAsync(requestUri).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
        }

        public Task<Result> CheckLimitNewDownline(int custId, byte custLevel, string language)
        {
            return PostAsJsonAsync<Result>("api/customer/CheckLimitNewDownline", new { custId, custLevel, language });
        }

        public Task<string> GetSuggestUsername(string actorName, int custId, byte roleId)
        {
            return PostAsJsonAsync<string>("api/customer/suggest-username", new { actorName, custId, roleId });
        }

        public Task<bool> CheckExistedCustomer(string username)
        {
            return GetAsJsonAsync<bool>($"api/customer/{username}/exist");
        }

        public Task<Downline> GetDownlineByUsername(int custId, int subAccountId, string username)
        {
            return GetAsJsonAsync<Downline>($"api/customer/{custId}/{subAccountId}/downlines/{username}");
        }
    }
}