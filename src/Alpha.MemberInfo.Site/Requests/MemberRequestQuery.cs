namespace Alpha.MemberInfo.Site.Requests.MemberRequest
{
    using MediatR;
    using ViewModels.MemberRequest;

    public class MemberRequestQuery : IRequest<MemberRequestViewModel>
    {
        public MemberRequestQuery()
        {
        }
    }
}