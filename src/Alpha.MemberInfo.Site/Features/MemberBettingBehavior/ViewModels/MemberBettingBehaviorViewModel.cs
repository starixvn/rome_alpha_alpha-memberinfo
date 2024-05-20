namespace Alpha.MemberInfo.Site.Features.MemberBettingBehavior.ViewModels
{
    using Alpha.MemberInfo.Features.MemberBettingBehavior.Models;

    public class MemberBettingBehaviorViewModel
    {
        public MemberBettingBehaviorViewModel() { }

        public CustomerProfile CustomerProfile { get; set; }

        public MemberBettingStatistic MemberBettingStatistic { get; set; }
    }
}