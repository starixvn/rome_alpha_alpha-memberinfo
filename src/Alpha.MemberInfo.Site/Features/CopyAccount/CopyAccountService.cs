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
                maxCredit = command.CreditMax,
                sourceCustId,
                sourceUsername = command.SourceUsername,
                numberOfAccounts = command.NumberOfAccounts,
                language = command.Language,
                ip = command.IP,
                systemId = 2
            };

            using (var stringContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json"))
            {
                using (var request = new HttpRequestMessage(HttpMethod.Post, "api/customer/copy") { Content = stringContent })
                {
                    using (var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false))
                    {
                        response.EnsureSuccessStatusCode();
                        return JsonConvert.DeserializeObject<CopyAccountResponse>(await response.Content.ReadAsStringAsync());
                    }
                }
            }

        }
    }
}