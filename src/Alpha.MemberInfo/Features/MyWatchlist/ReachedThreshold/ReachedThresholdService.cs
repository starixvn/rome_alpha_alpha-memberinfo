namespace Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold
{
    using Alpha.MemberInfo.Extensions;
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Criteria;
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Models;
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Params;
    using Alpha.Web.Helpers;
    using Fanex.Data.Repository;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ReachedThresholdService
    {
        Task<ReachlistAccountReport> GetReachlistAccounts(SelectReachListParam param);

        Task<IEnumerable<ReachHistoryModel>> GetReachDetail(SelectReachDetailParam param);
    }

    public class ReachedThresholdServiceImpl : ReachedThresholdService
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly PartialUMService partialUMService;

        public ReachedThresholdServiceImpl(
            IDynamicRepository dynamicRepository,
            PartialUMService partialUMService)
        {
            this.dynamicRepository = dynamicRepository;
            this.partialUMService = partialUMService;
        }

        public async Task<ReachlistAccountReport> GetReachlistAccounts(SelectReachListParam param)
        {
            var selectReachListByAgCriteria = new SelectReachListByAgCriteria(
                param.AgentId,
                param.FromDate.GetGreaterDate(param.LimitedDateToShowInfo),
                param.ToDate,
                param.LimitType);
            partialUMService.CheckPartialUM(selectReachListByAgCriteria.GetSettingKey());

            await dynamicRepository.ExecuteAsync(selectReachListByAgCriteria);

            var selectReachListCriteria = new SelectReachListCriteria(
                param.AgentId,
                param.Username,
                param.Status,
                param.SortType,
                param.IsDesc,
                param.PageSize,
                param.PageIndex,
                selectReachListByAgCriteria.XMLReachList ?? string.Empty);
            var reachlistAccounts = await dynamicRepository.FetchAsync<ReachlistAccountModel>(selectReachListCriteria);

            return new ReachlistAccountReport(reachlistAccounts, selectReachListCriteria.TotalItems);
        }

        public async Task<IEnumerable<ReachHistoryModel>> GetReachDetail(SelectReachDetailParam param)
        {
            var criteria = new SelectReachDetailCriteria(
                param.CustId,
                param.FromDate.GetGreaterDate(param.LimitedDateToShowInfo),
                param.ToDate,
                param.LimitType,
                param.SortType,
                param.IsDesc);
            partialUMService.CheckPartialUM(criteria.GetSettingKey());

            return await dynamicRepository.FetchAsync<ReachHistoryModel>(criteria);
        }
    }
}
