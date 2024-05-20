namespace Alpha.MemberInfo.Site.Features.MemberBettingBehavior.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.MemberBettingBehavior.ViewModels;
    using Alpha.MemberInfo.Site.Requests;
    using MediatR;

    public class MemberBettingBehaviorQuery : MemberInfoBaseQuery, IAsyncRequest<MemberBettingBehaviorViewModel>
    {
        public MemberBettingBehaviorQuery(IUser viewer) : base(viewer)
        {

        }
    }
}