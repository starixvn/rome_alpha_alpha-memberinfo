using Alpha.MemberInfo.Models;
using Alpha.MemberInfo.Site.Requests.LicenseeSignup;

namespace Alpha.MemberInfo.Site.ViewModels.LicenseeSignup
{
    public class LicenseeSignupViewModel
    {
        private readonly LicenseeSignupQuery query;

        public LicenseeSignupViewModel(LicenseeSignupQuery query)
        {
            this.query = query;
        }

        public UserLevel ActorLevel => query.ActorLevel;
    }
}