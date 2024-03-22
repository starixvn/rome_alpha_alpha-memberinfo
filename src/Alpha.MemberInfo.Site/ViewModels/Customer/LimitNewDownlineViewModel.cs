using Alpha.MemberInfo.Models;
using Alpha.MemberInfo.Site.Requests.Customer;

namespace Alpha.MemberInfo.Site.ViewModels.Customer
{
    public class LimitNewDownlineViewModel
    {
        public LimitNewDownlineViewModel(CreateCustomerQuery query, string message)
        {
            Message = message;
            ActorLevel = query.ActorLevel;
        }

        public string Message { get; }

        public UserLevel ActorLevel { get; }
    }
}