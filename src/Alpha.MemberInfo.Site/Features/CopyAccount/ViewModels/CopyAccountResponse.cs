namespace Alpha.MemberInfo.Site.Features.CopyAccount.ViewModels
{
    public class CopyAccountResponse
    {
        public string[] Usernames { get; set; }

        public bool Success { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }
    }
}