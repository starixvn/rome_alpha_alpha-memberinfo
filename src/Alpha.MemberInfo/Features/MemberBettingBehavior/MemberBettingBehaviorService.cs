namespace Alpha.MemberInfo.Features.MemberBettingBehavior
{
    using Alpha.MemberInfo.Features.MemberBettingBehavior.Criteria;
    using Alpha.MemberInfo.Features.MemberBettingBehavior.Models;
    using Alpha.MemberInfo.Services;
    using Fanex.Data.Repository;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public interface MemberBettingBehaviorService
    {
        Task<CustomerProfile> GetCustomerProfile(int custId);

        Task<MemberBettingStatistic> GetMemberBettingBehaviorStatistics(int custId, DateTime startDate, DateTime endDate);

        Task<BettingStatisticExportData> GetMemberBettingStatisticExcelData(int custId, DateTime startDate, DateTime endDate);
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

        public async Task<CustomerProfile> GetCustomerProfile(int custId)
        {
            var criteria = new GetCustomerProfileCriteria(custId);
            var customerProfile = await dynamicRepository.GetAsync<CustomerProfile>(criteria);
            if(criteria.ErrCode == 0)
            {
                customerProfile.Status = refService.GetCustomerProfileStatus(customerProfile.CategoryID);
                customerProfile.Level = refService.GetCustomerProfileLevel(customerProfile.CategoryID);
                customerProfile.RelatedToOtherAccount = true;
            }

            return customerProfile;
        }

        public async Task<MemberBettingStatistic> GetMemberBettingBehaviorStatistics(int custId, DateTime startDate, DateTime endDate)
        {
            var criteria = new GetMemberBettingStatisticsCriteria(custId, startDate, endDate);
            var (summaryStatistic, turnOverBySports, turnOverBybettypes, turnOverWinLossByDates) 
                = await dynamicRepository.FetchMultipleAsync<SummaryStatistic, TurnOverBySport, TurnoverByBettype, TurnoverWinLossByDate>(criteria);
            
            foreach (var item in turnOverBySports)
            {
                item.SportType = GetMemberStatisticSportTypeName(item.SportType);
            }

            foreach (var item in turnOverBybettypes)
            {
                item.Bettype = GetMemberStatisticBetTypeName(item.Bettype);
            }

            return new MemberBettingStatistic
            { 
                SummaryStatistic = summaryStatistic.FirstOrDefault(),
                TurnOverBySports = turnOverBySports,
                TurnoverByBettypes = turnOverBybettypes,
                TurnoverWinLossByDates = turnOverWinLossByDates
            };
        }

        public async Task<BettingStatisticExportData> GetMemberBettingStatisticExcelData(int custId, DateTime startDate, DateTime endDate)
        {
            var criteria = new GetMemberBettingStatisticsExportCriteria(custId, startDate, endDate);
            var (bettingStatisticExports, total) = await dynamicRepository.FetchMultipleAsync<BettingStatisticExportRow, BettingStatisticExportRow>(criteria);

            foreach(var item in bettingStatisticExports)
            {
                item.Sporttype = GetMemberStatisticSportTypeName(item.Sporttype);
                item.Bettype = GetMemberStatisticBetTypeName(item.Bettype);
            }

            return new BettingStatisticExportData {
                BettingStatisticExportRows = bettingStatisticExports,
                Total = total.FirstOrDefault()
            };
        }

        private string GetMemberStatisticSportTypeName(string sportType)
        {
            int.TryParse(sportType, out int sportTypeId);
            var sportTypeName = refService.GetSportTypeName(sportTypeId);
            return sportTypeName != "" ? sportTypeName : refService.GetCustBetBehavior(sportType);
        }

        private string GetMemberStatisticBetTypeName(string betType)
        {
            int.TryParse(betType, out int betTypeId);
            var betTypeName = refService.GetBetTypeName(betTypeId);
            return betTypeName != "" ? betTypeName : refService.GetCustBetBehavior(betType);
        }
    }
}
