using System.Net.Http;
using System.Threading.Tasks;
using Alpha.MemberInfo.Extensions;
using Alpha.MemberInfo.Models;

namespace Alpha.MemberInfo.Features.Shared
{
    public interface ICustomerService
    {

        Task<Result> CheckAllLimitedRules(int custId, byte custLevel, string actorUsername, string language);
    }

    public class CustomerService : ICustomerService
    {
        protected readonly HttpClient httpClient;

        public CustomerService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Task<Result> CheckAllLimitedRules(int custId, byte custLevel, string actorUsername, string language)
        {
            return httpClient.PostAsJsonAsync<Result>("api/rules/CheckAllLimitedRules", new { custId, custLevel, actorUsername, language });
        }
    }
}
