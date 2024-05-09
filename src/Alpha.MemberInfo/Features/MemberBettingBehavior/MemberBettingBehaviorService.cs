
namespace Alpha.MemberInfo.Features.MemberBettingBehavior
{
    using Alpha.MemberInfo.Features.MemberBettingBehavior.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface MemberBettingBehaviorService
    {
        Task<IEnumerable<MemberBettingStatistic>> GetMemberBettinBehaviorStatistics();
    }

    public class MemberBettingBehaviorServiceImpl : MemberBettingBehaviorService
    {
        public Task<IEnumerable<MemberBettingStatistic>> GetMemberBettinBehaviorStatistics()
        {
            var betCount = new BetCount { Win = 1, Lose = 1, Draw = 1, Canceled = 1, Total = 4 };
            var winLoss = new WinLoss { Winning = 1, Losing = 1, Total = 1, Margin = 2 };
            var stake = new Stake { Average = 1, TurnoverLive = 1, TurnoverNonLive = 1, TotalTurnover = 3, NetTurnover = 4 };
            var temp = new MemberBettingStatistic { Sports = "Soccer", BetType = "Handicap", BetCount = betCount, WinLoss = winLoss, Stake = stake };

            return Task.FromResult<IEnumerable<MemberBettingStatistic>>(new[] { temp, temp, temp });
        }
    }
}
