namespace Alpha.MemberInfo.Services.CreditBalance
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Criteria.CreditBalance;
    using Fanex.Data.Repository;
    using Models.CreditBalance;
    using Params;
    using Sunplus.Core.Common.IPs;

    public interface CreditBalanceService
    {
        Task<PagedCreditBalance> GetPagedCreditBalancesAsync(CreditBalanceParam param);

        Task<PagedCreditBalance> GetCreditBalancesAsync(CreditBalanceParam param);

        Task<IDictionary<int, byte>> GetClosedReasonsAsync(CreditBalanceParam param);

        Task<IDictionary<int, decimal>> GetAgentNetPositionsAsync(CreditBalanceParam param);
    }

    public class CreditBalanceServiceImpl : CreditBalanceService
    {
        private readonly IDynamicRepository repository;

        public CreditBalanceServiceImpl(IDynamicRepository dynamicRepository)
        {
            repository = dynamicRepository;
        }

        public async Task<PagedCreditBalance> GetPagedCreditBalancesAsync(CreditBalanceParam param)
        {
            var closedReasons = await GetClosedReasonsAsync(param);
            var agentNetPositions = await GetAgentNetPositionsAsync(param);
            var pagedCreditBalance = await GetCreditBalancesAsync(param);

            foreach (var item in pagedCreditBalance.CreditBalances)
            {
                item.IPLogin = GetIPFormatInstance(item.IPLogin, param.AdminId, param.IsAdminCanViewVNIP).Format();

                if (item.Closed && closedReasons.ContainsKey(item.CustId))
                {
                    item.ClosedReason = closedReasons[item.CustId];
                }

                if (agentNetPositions.ContainsKey(item.CustId))
                {
                    item.BetCredit += agentNetPositions[item.CustId];
                }
            }

            return pagedCreditBalance;
        }

        public async Task<PagedCreditBalance> GetCreditBalancesAsync(CreditBalanceParam param)
        {
            GetPagedCreditBalanceCriteria criteria = null;

            if (param.CustLevel.IsSuperLevel())
            {
                criteria = GetSuperCreditBalanceCriteria(param);
            }
            else if (param.CustLevel.IsMasterLevel())
            {
                criteria = GetMasterCreditBalanceCriteria(param);
            }
            else if (param.CustLevel.IsAgentLevel())
            {
                criteria = GetAgentCreditBalanceCriteria(param);
            }
            else
            {
                return new PagedCreditBalance(
                    param.PageIndex,
                    param.PageSize,
                    0,
                    Enumerable.Empty<CreditBalanceItem>());
            }

            var creditBalances = await repository.FetchAsync<CreditBalanceItem>(criteria);

            return new PagedCreditBalance(
                criteria.PageIndex,
                criteria.PageSize,
                criteria.TotalItems,
                creditBalances);
        }

        private static IPFormat GetIPFormatInstance(string ip, int adminId, bool isAdminCanViewVNIP)
            => new IPFormater(ip, adminId, isAdminCanViewVNIP);

        private static GetPagedCreditBalanceCriteria GetSuperCreditBalanceCriteria(CreditBalanceParam param)
            => new GetSuperCreditBalanceCriteria(
                param.CustId,
                param.UserName,
                param.Enabled,
                param.Closed,
                param.Suspended,
                param.PageIndex,
                param.PageSize);

        private static GetPagedCreditBalanceCriteria GetMasterCreditBalanceCriteria(CreditBalanceParam param)
            => new GetMasterCreditBalanceCriteria(
                param.CustId,
                param.UserName,
                param.Enabled,
                param.Closed,
                param.Suspended,
                param.PageIndex,
                param.PageSize);

        private static GetPagedCreditBalanceCriteria GetAgentCreditBalanceCriteria(CreditBalanceParam param)
            => new GetAgentCreditBalanceCriteria(
                param.CustId,
                param.UserName,
                param.Enabled,
                param.Closed,
                param.Suspended,
                param.PageIndex,
                param.PageSize);

        public async Task<IDictionary<int, byte>> GetClosedReasonsAsync(CreditBalanceParam param)
        {
            ICriteria criteria;

            if (param.CustLevel.IsSuperLevel())
            {
                criteria = GetSuperClosedReasonCriteria(param);
            }
            else if (param.CustLevel.IsMasterLevel())
            {
                criteria = GetMasterClosedReasonCriteria(param);
            }
            else if (param.CustLevel.IsAgentLevel())
            {
                criteria = GetAgentClosedReasonCriteria(param);
            }
            else
            {
                return new Dictionary<int, byte>();
            }

            var closedReasons = await repository.FetchAsync<CustomerClosedReason>(criteria);

            return closedReasons.ToDictionary(kvp => kvp.CustId, kvp => kvp.ClosedReason);
        }

        private static GetSuperClosedReasonCriteria GetSuperClosedReasonCriteria(CreditBalanceParam param)
            => new GetSuperClosedReasonCriteria(
                param.CustId,
                param.UserName,
                param.Enabled,
                param.Closed,
                param.Suspended,
                param.PageIndex,
                param.PageSize);

        private static GetMasterClosedReasonCriteria GetMasterClosedReasonCriteria(CreditBalanceParam param)
            => new GetMasterClosedReasonCriteria(
                param.CustId,
                param.UserName,
                param.Enabled,
                param.Closed,
                param.Suspended,
                param.PageIndex,
                param.PageSize);

        private static GetAgentClosedReasonCriteria GetAgentClosedReasonCriteria(CreditBalanceParam param)
            => new GetAgentClosedReasonCriteria(
                param.CustId,
                param.UserName,
                param.Enabled,
                param.Closed,
                param.Suspended,
                param.PageIndex,
                param.PageSize);

        public async Task<IDictionary<int, decimal>> GetAgentNetPositionsAsync(CreditBalanceParam param)
        {
            if (param.CustLevel.IsNotAgentLevel())
            {
                return new Dictionary<int, decimal>();
            }

            var criteria = GetAgentNetPositionCriteria(param);
            var agentNetPositions = await repository.FetchAsync<AgentNetPosition>(criteria);

            return agentNetPositions.ToDictionary(kvp => kvp.CustId, kvp => kvp.NetPosition);
        }

        private static ICriteria GetAgentNetPositionCriteria(CreditBalanceParam param)
            => new GetAgentNetPositionCriteria(
                param.CustId,
                param.UserName,
                param.Enabled,
                param.Closed,
                param.Suspended,
                param.PageIndex,
                param.PageSize);
    }
}
