using System.Collections.Generic;

namespace Alpha.MemberInfo.Site.Features.CopyAccount.ViewModels
{
    public class CopyAccountResponse
    {
        public IEnumerable<string> Usernames { get; set; }

        public bool Success { get; set; }

        public int Code { get; set; }

        public string Message { get; set; }

        public bool IsSuccessful => Code == 0;
    }
}