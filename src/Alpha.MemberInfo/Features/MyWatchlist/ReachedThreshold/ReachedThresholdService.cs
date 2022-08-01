namespace Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold
{
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Criteria;
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Models;
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Params;
    using Fanex.Data.Repository;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ReachedThresholdService
    {
        Task<ReachlistAccountDTO> GetReachlistAccounts(SelectReachListParam param);
        Task<IEnumerable<ReachHistoryModel>> GetReachDetail(SelectReachDetailParam param);
    }

    public class ReachedThresholdServiceImpl : ReachedThresholdService
    {
        private readonly IDynamicRepository dynamicRepository;

        public ReachedThresholdServiceImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public async Task<ReachlistAccountDTO> GetReachlistAccounts(SelectReachListParam param)
        {
            var criteria = new SelectReachListCriteria(
                param.AgentId,
                param.Username,
                param.FromDate,
                param.ToDate,
                param.Status,
                param.LimitType,
                param.SortType,
                param.IsDesc,
                param.PageSize,
                param.PageIndex);

            var reachlistAccounts = await dynamicRepository.FetchAsync<ReachlistAccountModel>(criteria);

            return new ReachlistAccountDTO(reachlistAccounts, criteria.TotalItems);
        }

        public async Task<IEnumerable<ReachHistoryModel>> GetReachDetail(SelectReachDetailParam param)
        {
            var criteria = new SelectReachDetailCriteria(
                param.CustId,
                param.FromDate,
                param.ToDate,
                param.LimitType);

            return await dynamicRepository.FetchAsync<ReachHistoryModel>(criteria);
        }
    }
}
