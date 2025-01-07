namespace Alpha.MemberInfo.Services.WinLossLimit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Criteria;
    using Criteria.WinLossLimit;
    using Fanex.Data.Repository;
    using Models;
    using Models.WinLossLimit;
    using Params;

    public interface WinLossLimitService
    {
        IEnumerable<WinLossLimitGroupProductModel> GetWinLossLimitProducts(int custId);

        IEnumerable<CustomerInfo> GetMasterList(int custId);

        IEnumerable<CustomerInfo> GetAgentList(int custId);

        WinLossLimitPage GetWinLossLimitPage(WinLossLimitParam param);
    }

    public class WinLossLimitServiceImpl : WinLossLimitService
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly Func<int, string> getProductName;
        private readonly Func<int, string> getSportTypeName;

        public WinLossLimitServiceImpl(
            IDynamicRepository dynamicRepository,
            Func<int, string> getProductName,
            Func<int, string> getSportTypeName)
        {
            this.dynamicRepository = dynamicRepository;
            this.getProductName = getProductName;
            this.getSportTypeName = getSportTypeName;
        }

        public IEnumerable<WinLossLimitGroupProductModel> GetWinLossLimitProducts(int custId)
        {
            var groupProducts = dynamicRepository
                .Fetch<WinLossLimitProductModel>(new GetWinLossLimitProductCriteria(custId))
                .GroupBy(product => product.Type);

            foreach (var group in groupProducts)
            {
                foreach (var product in group)
                {
                    product.ProductName = getSportTypeName(product.SportType);
                }

                yield return new WinLossLimitGroupProductModel(
                    group.Key == 0 ? string.Empty : getProductName(group.Key),
                    group);
            }
        }

        public IEnumerable<CustomerInfo> GetMasterList(int custId)
            => dynamicRepository.Fetch<CustomerInfo>(new GetMasterListCriteria(custId));

        public IEnumerable<CustomerInfo> GetAgentList(int custId)
            => dynamicRepository.Fetch<CustomerInfo>(new GetAgentListCriteria(custId));

        public WinLossLimitPage GetWinLossLimitPage(WinLossLimitParam param)
        {
            var result = new WinLossLimitPage();
            var playerIds = !string.IsNullOrWhiteSpace(param.PlayerId) ?
                MapPlayerIdToCustId(new MapPlayerId3rdToCustIdCriteria(param.ProductId, param.PlayerId, param.CustId, param.MasterId, param.AgentId))
                : Enumerable.Empty<int>();

            if (!playerIds.Any() && string.IsNullOrWhiteSpace(param.PlayerId))
            {
                result += GetWinLossLimits(new GetCustWinLossLimitCriteria(
                    param.CustId,
                    param.ProductId,
                    param.BetType,
                    param.SportType,
                    param.Username,
                    param.MasterId,
                    param.AgentId,
                    param.PageIndex,
                    param.PageSize,
                    null,
                    param.IsDirectMember));
            }

            foreach (var playerId in playerIds)
            {
                result += GetWinLossLimits(new GetCustWinLossLimitCriteria(
                    param.CustId,
                    param.ProductId,
                    param.BetType,
                    param.SportType,
                    param.Username,
                    param.MasterId,
                    param.AgentId,
                    param.PageIndex,
                    param.PageSize,
                    playerId,
                    param.IsDirectMember));
            }

            if (!result.WinLossLimitDetail.Any())
            {
                return result;
            }

            var balances = dynamicRepository.Fetch<WinLossLimitDetail>(new GetBalanceCriteria(
                param.CustId,
                param.BetType,
                param.Username,
                param.MasterId,
                param.AgentId,
                param.PageIndex,
                param.PageSize), param.AGIP);
            IEnumerable<WinLossLimitDetail> outstandings = Enumerable.Empty<WinLossLimitDetail>();

            foreach (var custWinLossLimit in result.WinLossLimitDetail)
            {
                var balance = balances.FirstOrDefault(item => item.CustId == custWinLossLimit.CustId);
                if (balance != null)
                {
                    custWinLossLimit.CreditBalance = balance.CreditBalance;
                    custWinLossLimit.DebitBalance = balance.DebitBalance;
                    custWinLossLimit.Balance = balance.Balance;
                    custWinLossLimit.Transfer3rd = balance.Transfer3rd;
                }

                var outstanding = outstandings.FirstOrDefault(item => item.CustId == custWinLossLimit.CustId);
                if (outstanding != null)
                {
                    custWinLossLimit.Outstanding = outstanding.Outstanding;
                }
            }

            return result;
        }

        private IEnumerable<int> MapPlayerIdToCustId(MapPlayerId3rdToCustIdCriteria criteria)
            => dynamicRepository.Fetch<int>(criteria);

        private WinLossLimitPage GetWinLossLimits(GetCustWinLossLimitCriteria criteria)
        {
            var winLossLimitDetail = dynamicRepository.Fetch<WinLossLimitDetail>(criteria);

            return new WinLossLimitPage(criteria.TotalRows, winLossLimitDetail);
        }
    }
}