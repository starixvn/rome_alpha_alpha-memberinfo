namespace Alpha.MemberInfo.Site.Features.MemberBettingBehavior.Requests
{
    using Alpha.MemberInfo.Features.MemberBettingBehavior.Models;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Requests;
    using MediatR;

    public class MemberBettingStatisticQuery : MemberInfoBaseQuery, IAsyncRequest<MemberBettingStatistic>
    {
        public MemberBettingStatisticQuery(IUser viewer) : base(viewer)
        {
        }

        public string UserName { get; set; }

        public int DateRange { get; set; }
    }
}