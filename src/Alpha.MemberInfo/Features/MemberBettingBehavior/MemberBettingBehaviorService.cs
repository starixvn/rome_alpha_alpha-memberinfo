
namespace Alpha.MemberInfo.Features.MemberBettingBehavior
{
    using Alpha.MemberInfo.Features.MemberBettingBehavior.Criteria;
    using Alpha.MemberInfo.Features.MemberBettingBehavior.Models;
    using Alpha.MemberInfo.Services;
    using Fanex.Data.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface MemberBettingBehaviorService
    {
        Task<CustomerProfile> GetCustomerProfile(string username);

        Task<MemberBettingStatistic> GetMemberBettingBehaviorStatistics();

        Task<BettingStatisticExportData> GetMemberBettingStatisticExcelData();
    }

    public class MemberBettingBehaviorServiceImpl : MemberBettingBehaviorService
    {
        private readonly IDynamicRepository dynamicRepository;

        private readonly CacheRefService refService;

        public MemberBettingBehaviorServiceImpl(IDynamicRepository dynamicRepository, CacheRefService refService)
        {
            this.dynamicRepository = dynamicRepository;
            this.refService = refService;
        }

        public async Task<CustomerProfile> GetCustomerProfile(string username)
        {
            var criteria = new GetCustomerProfileCriteria(117834959);
            var customerProfile = await dynamicRepository.GetAsync<CustomerProfile>(criteria);
            //customerProfile.Status = refService.GetCustomerProfileStatus(54);
            customerProfile.Status = "Arbitrage";
            customerProfile.Level = "Observation";
            customerProfile.RelatedToOtherAccount = true;

            return customerProfile;
        }

        public async Task<MemberBettingStatistic> GetMemberBettingBehaviorStatistics()
        {
            var criteria = new GetMemberBettingStatisticsCriteria(119272758, new DateTime(2024, 4, 14), new DateTime(2024, 5, 14));
            var rawdata = await dynamicRepository.FetchMultipleAsync<SummaryStatistic, TurnOverBySport, TurnoverByBettype, TurnoverWinLossByDate>(criteria);

            var summaryStatistic = new SummaryStatistic();
            var turnOverBySports = new List<TurnOverBySport>();
            var turnOverBybettypes = new List<TurnoverByBettype>();
            var turnOverWinLossByDates = new List<TurnoverWinLossByDate>();

            if (rawdata.Item1.Any())
            {
                summaryStatistic = rawdata.Item1.FirstOrDefault();
            }
            if (rawdata.Item2.Any())
            {
                turnOverBySports.AddRange(rawdata.Item2);
            }
            if (rawdata.Item3.Any())
            {
                turnOverBybettypes.AddRange(rawdata.Item3);
            }
            if (rawdata.Item4.Any())
            {
                turnOverWinLossByDates.AddRange(rawdata.Item4);
            }

            return new MemberBettingStatistic
            { 
                SummaryStatistic = summaryStatistic,
                TurnOverBySports = turnOverBySports,
                TurnoverByBettypes = turnOverBybettypes,
                TurnoverWinLossByDates = turnOverWinLossByDates
            };
        }

        public async Task<BettingStatisticExportData> GetMemberBettingStatisticExcelData()
        {
            var criteria = new GetMemberBettingStatisticsExportCriteria(119272758, new DateTime(2024, 4, 14), new DateTime(2024, 5, 14));
            var rawdata = await dynamicRepository.FetchMultipleAsync<BettingStatisticExportRow, BettingStatisticExportRow>(criteria);
            var bettingStatisticExports = new List<BettingStatisticExportRow>();
            var total = new BettingStatisticExportRow();

            if (rawdata.Item1.Any())
            {
                bettingStatisticExports.AddRange(rawdata.Item1);
            }
            if (rawdata.Item2.Any())
            {
                total = rawdata.Item2.FirstOrDefault();
            }

            return new BettingStatisticExportData {
                BettingStatisticExportRows = bettingStatisticExports,
                Total = total
            };
        }
    }
}
