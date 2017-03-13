namespace Alpha.MemberInfo.Site.ViewModels.MemberRequest
{
    using System.Collections.Generic;
    using MemberInfo.Models;

    public class MemberRequestViewModel
    {
        public MemberRequestViewModel(IEnumerable<MemberRequestModel> memberRequestData)
        {
            MemberRequestData = memberRequestData;
        }

        public IEnumerable<MemberRequestModel> MemberRequestData { get; }
    }
}