namespace Alpha.MemberInfo.Site.Requests.MemberRequest
{
    using MediatR;
    using MemberInfo.Models;
    using ViewModels.MemberRequest;

    public class MemberRequestQuery : IRequest<MemberRequestViewModel>
    {
        public MemberRequestQuery(IUser viewer)
        {
        }
    }
}