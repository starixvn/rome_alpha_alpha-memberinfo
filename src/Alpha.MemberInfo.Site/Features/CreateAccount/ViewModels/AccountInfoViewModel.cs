using System;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.MemberInfo.Site.Features.CreateAccount.ViewModels
{
    public class AccountInfoViewModel
    {
        public string Usernames { get; set; } = String.Empty;
        
        public decimal Credit { get; set; }
        
        public IEnumerable<string> Domains { get; set; } = Enumerable.Empty<string>();
    }
}