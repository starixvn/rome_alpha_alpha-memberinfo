using System.Collections.Generic;
using System.Linq;
using Alpha.MemberInfo.Models;

namespace Alpha.MemberInfo.Site.Requests.Customer
{
    public class AccountInfoQuery : MemberInfoBaseQuery
    {
        public AccountInfoQuery(IUser viewer): base(viewer) { }
        
        public IEnumerable<string> Domains { get; set; } = Enumerable.Empty<string>();
    }
}