namespace Alpha.MemberInfo.Features.MemberBettingBehavior
{
    using Alpha.MemberInfo.Extensions;
    using Alpha.MemberInfo.Features.MemberBettingBehavior.Criteria;
    using Alpha.MemberInfo.Features.MemberBettingBehavior.Models;
    using Alpha.MemberInfo.Services;
    using AlphaPlus.PartialUM.Services;
    using Fanex.Caching;
    using Fanex.Data.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Fanex.Logging.Extensions.Web;

    public interface MemberBettingBehaviorService
    {
        Task<CustomerProfile> GetCustomerProfile(int custId, string siteName);

        Task<MemberBettingStatistic> GetMemberBettingBehaviorStatistics(int custId, DateTime startDate, DateTime endDate, string siteName, string language);

        Task<BettingStatisticExportData> GetMemberBettingStatisticExcelData(int custId, DateTime startDate, DateTime endDate, string siteName, string language);
    }

    public class MemberBettingBehaviorServiceImpl : MemberBettingBehaviorService
    {
        private const string StatisticCacheKey = "Alpha.MemberInfo.Features.MemberBettingBehavior.Statistic";

        private const string StatisticExportCacheKey = "Alpha.MemberInfo.Features.MemberBettingBehavior.StatisticExport";

        private const byte MinuteTimeCache = 5;

        private readonly IDynamicRepository dynamicRepository;

        private readonly ICacheService cacheService;

        private readonly CacheRefService refService;

        private readonly IPartialUMService partialUMService;

        private readonly HttpClient httpClient;

        public MemberBettingBehaviorServiceImpl(
            IDynamicRepository dynamicRepository,
            ICacheService cacheService,
            IPartialUMService partialUMService,
            CacheRefService refService,
            HttpClient httpClient)
        {
            this.dynamicRepository = dynamicRepository;
            this.cacheService = cacheService;
            this.partialUMService = partialUMService;
            this.refService = refService;
            this.httpClient = httpClient;
        }

        public async Task<CustomerProfile> GetCustomerProfile(int custId, string siteName)
        {
            var criteria = new GetCustomerProfileCriteria(custId);
            partialUMService.ThrowIfUM(criteria.GetSettingKey(), siteName);
            var customerProfile = await dynamicRepository.GetAsync<CustomerProfile>(criteria);
            if(customerProfile != null)
            {
                customerProfile.Status = refService.GetCustomerProfileStatus(customerProfile.CategoryID);
                customerProfile.Level = refService.GetCustomerProfileLevel(customerProfile.CategoryID);
                customerProfile.RelatedToOtherAccount = customerProfile.IsProblemAccount;
                if(!customerProfile.RelatedToOtherAccount)
                {
                    try
                    {
                        var customerAssociation = await httpClient.GetAsJsonAsync<CustomerAssociation>($"cts/Association/CheckByCustId?custId={custId}");
                        customerProfile.RelatedToOtherAccount = customerAssociation.IsAssociated;
                    } catch(Exception ex)
                    {
                        ExceptionHandler.Handle(ex);
                    }
                }
            }


            return customerProfile;
        }

        public async Task<MemberBettingStatistic> GetMemberBettingBehaviorStatistics(int custId, DateTime startDate, DateTime endDate, string siteName, string language)
        {
            var criteria = new GetMemberBettingStatisticsCriteria(custId, startDate, endDate);
            partialUMService.ThrowIfUM(criteria.GetSettingKey(), siteName);

            SummaryStatistic summaryStatistic;
            var turnOverBySports = new List<TurnOverBySport>();
            var turnOverBybettypes = new List<TurnoverByBettype>();
            var turnOverWinLossByDates = new List<TurnoverWinLossByDate>();

            var cacheKey = $"{StatisticCacheKey}.{language}.{custId}.{startDate.Day}.{endDate.Day}";
            var memberStatisticCacheData = await cacheService.GetAsync<MemberBettingStatistic>(cacheKey);

            if(memberStatisticCacheData != null)
            {
                summaryStatistic = memberStatisticCacheData.SummaryStatistic;
                turnOverBySports.AddRange(memberStatisticCacheData.TurnOverBySports);
                turnOverBybettypes.AddRange(memberStatisticCacheData.TurnoverByBettypes);
                turnOverWinLossByDates.AddRange(memberStatisticCacheData.TurnoverWinLossByDates);

                return new MemberBettingStatistic(summaryStatistic, turnOverBySports, turnOverBybettypes, turnOverWinLossByDates);
            } else {
                var rawData = await dynamicRepository.FetchMultipleAsync<SummaryStatistic, TurnOverBySport, TurnoverByBettype, TurnoverWinLossByDate>(criteria);
                summaryStatistic = rawData.Item1.FirstOrDefault();
                turnOverBySports.AddRange(rawData.Item2);
                turnOverBybettypes.AddRange(rawData.Item3);
                turnOverWinLossByDates.AddRange(rawData.Item4);
            }
            
            foreach (var item in turnOverBySports)
            {
                item.SportType = GetMemberStatisticSportTypeName(item.SportType);
            }

            foreach (var item in turnOverBybettypes)
            {
                int.TryParse(item.ProductID, out int productId);
                item.Bettype = GetMemberStatisticBetTypeName(item.Bettype);
                item.ProductName = refService.GetProductName(productId);
            }

            await cacheService.SetAsync(
                cacheKey,
                new MemberBettingStatistic(summaryStatistic, turnOverBySports, turnOverBybettypes, turnOverWinLossByDates),
                new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(MinuteTimeCache)));

            return new MemberBettingStatistic(summaryStatistic, turnOverBySports, turnOverBybettypes, turnOverWinLossByDates);
        }

        public async Task<BettingStatisticExportData> GetMemberBettingStatisticExcelData(int custId, DateTime startDate, DateTime endDate, string siteName, string language)
        {
            var criteria = new GetMemberBettingStatisticsExportCriteria(custId, startDate, endDate);
            partialUMService.ThrowIfUM(criteria.GetSettingKey(), siteName);

            var bettingStatisticExports = new List<BettingStatisticExportRow>();
            BettingStatisticExportRow total;

            var cacheKey = $"{StatisticExportCacheKey}.{language}.{custId}.{startDate.Day}.{endDate.Day}";
            var memberStatisticExportCacheData = await cacheService.GetAsync<BettingStatisticExportData>(cacheKey);

            if(memberStatisticExportCacheData != null)
            {
                bettingStatisticExports.AddRange(memberStatisticExportCacheData.BettingStatisticExportRows);
                total = memberStatisticExportCacheData.Total;
                return new BettingStatisticExportData(bettingStatisticExports, total);
            } else
            {
                var rawData = await dynamicRepository.FetchMultipleAsync<BettingStatisticExportRow, BettingStatisticExportRow>(criteria);
                bettingStatisticExports.AddRange(rawData.Item1);
                total = rawData.Item2.FirstOrDefault();
            }

            foreach(var item in bettingStatisticExports)
            {
                item.Sporttype = GetMemberStatisticSportTypeName(item.Sporttype);
                item.Bettype = GetMemberStatisticBetTypeName(item.Bettype);
            }

            await cacheService.SetAsync(
                  cacheKey,
                  new BettingStatisticExportData(bettingStatisticExports, total),
                  new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(MinuteTimeCache)));

            return new BettingStatisticExportData(bettingStatisticExports, total);
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
