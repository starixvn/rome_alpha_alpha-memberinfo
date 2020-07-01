namespace Alpha.MemberInfo.Site.ViewModels.IndirectLoginAccount
{
    using System.Collections.Generic;
    using System.Linq;
    using Alpha.MemberInfo.Models.IndirectLoginAccount;
    using Alpha.Web.Helpers;

    public class ManageAccountViewModel
    {
        public ManageAccountViewModel()
            : this(Enumerable.Empty<IndirectLoginAccountItem>())
        {
        }

        public ManageAccountViewModel(IEnumerable<IndirectLoginAccountItem> accounts)
        {
            Accounts = accounts;
        }

        public IEnumerable<IndirectLoginAccountItem> Accounts { get; }

        public string RootURL => MicroSites.MemberInfoUrl("~/IndirectLoginAccount/");
    }
}