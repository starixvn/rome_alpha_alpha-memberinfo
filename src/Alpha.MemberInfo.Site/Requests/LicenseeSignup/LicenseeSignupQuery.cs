using Alpha.MemberInfo.Models;

namespace Alpha.MemberInfo.Site.Requests.LicenseeSignup
{
    public class LicenseeSignupQuery : MemberInfoBaseQuery
    {
        public LicenseeSignupQuery(IUser viewer)
            : base(viewer)
        {
        }
    }
}