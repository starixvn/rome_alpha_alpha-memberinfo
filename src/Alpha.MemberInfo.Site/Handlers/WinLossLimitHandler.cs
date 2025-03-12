namespace Alpha.MemberInfo.Site.Handlers
{
    using System.Collections.Generic;
    using System.Linq;
    using Alpha.MemberInfo.AppResources;
    using MediatR;
    using MemberInfo.Models;
    using MemberInfo.Models.WinLossLimit;
    using Params;
    using Requests.WinLossLimit;
    using Services.WinLossLimit;
    using ViewModels.WinLossLimit;

    public class WinLossLimitHandler : IRequestHandler<WinLossLimitQuery, WinLossLimitViewModel>
    {
        private readonly WinLossLimitService winLossLimitService;

        public WinLossLimitHandler(WinLossLimitService winLossLimitService)
        {
            this.winLossLimitService = winLossLimitService;
        }

        public WinLossLimitViewModel Handle(WinLossLimitQuery message)
        {
            var products = winLossLimitService.GetWinLossLimitProducts(message.CustId).ToList();
            var firstProduct = products?.FirstOrDefault()?.Products?.FirstOrDefault();

            var actorLevel = message.ActorLevel;
            var masterList = new List<CustomerInfo>();
            if (actorLevel.IsSuperLevel())
            {
                masterList = winLossLimitService.GetMasterList(message.CustId).ToList();
                masterList.Insert(0, new CustomerInfo(Resources.DirectMember, 0));
            }

            var masterId = GetDefaultMasterId(message, actorLevel, masterList);
            var agentList = masterId == 0 ? new List<CustomerInfo>() : winLossLimitService.GetAgentList(masterId).ToList();
            if (actorLevel.IsMasterLevel())
            {
                agentList.Insert(0, new CustomerInfo(Resources.DirectMember, 0));
            }

            var agentId = GetDefaultAgentId(message, actorLevel, agentList);
            var param = new WinLossLimitParam(
                message.UserSelectedProductId == 0 && firstProduct != null ? firstProduct.ProductId : message.UserSelectedProductId,
                message.CustId,
                message.BetType == 0 && firstProduct != null ? firstProduct.BetType : message.BetType,
                message.SportType == 0 && firstProduct != null ? firstProduct.SportType : message.SportType,
                message.Username,
                message.PlayerId,
                masterId,
                agentId,
                message.PageSize,
                message.PageIndex,
                GetIsDirectMemberParam(actorLevel, masterId, agentId),
                message.AGIP);

            var noNeedGetData = !param.IsDirectMember && actorLevel.IsMasterOrSuperLevel() && !agentList.Any();
            var winLossLimitPage = noNeedGetData ? new WinLossLimitPage() : winLossLimitService.GetWinLossLimitPage(param);
            var viewModel = new WinLossLimitViewModel(message, products, masterList, agentList, winLossLimitPage, param);

            return viewModel;
        }

        private static int GetDefaultMasterId(WinLossLimitQuery message, UserLevel actorLevel, List<CustomerInfo> masterList)
        {
            if (message.MasterId == -1)
            {
                if (actorLevel.IsSuperLevel() && masterList.Count > 1)
                {
                    // First item is direct member
                    // Second item is the first master
                    return masterList[1].CustId;
                }

                if (actorLevel.IsMasterLevel())
                {
                    return message.CustId;
                }

                return 0;
            }

            return message.MasterId;
        }

        private static int GetDefaultAgentId(WinLossLimitQuery message, UserLevel actorLevel, List<CustomerInfo> agentList)
        {
            if (message.RecommendId == -1)
            {
                if (actorLevel.IsMasterLevel() && agentList.Count > 1)
                {
                    // First item is direct member
                    // Second item is the first agent
                    return agentList[1].CustId;
                }

                if (actorLevel.IsAgentLevel())
                {
                    return message.CustId;
                }

                return 0;
            }

            return message.RecommendId;
        }

        private static bool GetIsDirectMemberParam(UserLevel actorLevel, int masterId, int agentId)
        {
            if (actorLevel.IsSuperLevel())
            {
                return masterId == 0;
            }

            if (actorLevel.IsMasterLevel())
            {
                return agentId == 0;
            }

            return false;
        }
    }
}
