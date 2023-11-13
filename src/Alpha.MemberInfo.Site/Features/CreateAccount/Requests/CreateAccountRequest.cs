using Fanex.Customer.Client.Models;

namespace Alpha.MemberInfo.Site.Features.CreateAccount.Requests
{
    public class CreateAccountRequest
    {
        public string UIBehavior { get; set; }

        public GeneralInfo GeneralInfo { get; set; }

        public string ProductGroups { get; set; }
    }
}