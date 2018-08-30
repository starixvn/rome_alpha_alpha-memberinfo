namespace Alpha.MemberInfo.Site.ViewModels.WinLossLimit
{
    using System.Collections.Generic;
    using AppResources;
    using Components;
    using MemberInfo.Helpers;
    using MemberInfo.Models;
    using MemberInfo.Models.WinLossLimit;
    using Params;
    using Requests.WinLossLimit;

    public class WinLossLimitViewModel
    {
        public WinLossLimitViewModel(
            WinLossLimitQuery message,
            IEnumerable<WinLossLimitProductModel> products,
            IEnumerable<CustomerInfo> masterList,
            IEnumerable<CustomerInfo> agentList,
            WinLossLimitPage winLossLimitPage,
            WinLossLimitParam param)
        {
            ViewerLevel = message.ActorLevel;
            UserSelectedProductId = param.ProductId;
            Username = message.Username ?? string.Empty;
            PlayerId = message.PlayerId ?? string.Empty;
            MasterId = param.MasterId;
            AgentId = param.AgentId;

            Products = products;
            IsPokerGames = UserSelectedProductId.IsPokerGames();
            MasterList = masterList;
            AgentList = agentList;

            PageSize = message.PageSize;
            PageIndex = message.PageIndex;
            WinLossLimitPage = winLossLimitPage;

            SportType = param.SportType;
            BetType = param.BetType;
        }

        public UserLevel ViewerLevel { get; }

        public int UserSelectedProductId { get; }

        public IEnumerable<WinLossLimitProductModel> Products { get; }

        public bool IsPokerGames { get; }

        public int MasterId { get; }

        public int AgentId { get; }

        public string Username { get; }

        public string PlayerId { get; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public int SportType { get; }

        public int BetType { get; }

        public IEnumerable<CustomerInfo> MasterList { get; }

        public IEnumerable<CustomerInfo> AgentList { get; }

        public WinLossLimitPage WinLossLimitPage { get; }

        public PagerViewModel PagerViewModel =>
            new PagerViewModel(PageIndex, PageSize, WinLossLimitPage.NumberOfRows);

        public bool HasData => WinLossLimitPage.NumberOfRows > 0;

        public bool ShowPager => WinLossLimitPage.NumberOfRows >= 100;

        public int NumberOfColumns => IsPokerGames ? 15 : 13;

        public static string GetPlayerIdText(string playerId)
            => string.IsNullOrWhiteSpace(playerId) ? "-" : playerId;

        public static string GetStatusText(string statusKey) => Resources.ResourceManager.GetString(statusKey);

        public static string GetCurrentWinAnmout(decimal? currentWinAnmout)
            => currentWinAnmout.HasValue ? currentWinAnmout.Value.ToString("#,##0.00") : "Unlimited";

        public static string GetMaxWinLimit(decimal maxWinLimit)
            => maxWinLimit == 0 ? "Unlimited" : maxWinLimit.ToString("#,##0") + "X";

        public static string GetWinPercent(decimal? winPercent)
            => !winPercent.HasValue
            ? "Unlimited"
            : winPercent > 0 ? winPercent.Value.ToString("#,##0.##") + "%" : "0%";

        public static string GetLossPercent(decimal? maxLossLimit, decimal? lossPercent)
            => !maxLossLimit.HasValue ? "-"
            : !lossPercent.HasValue ? "Unlimited"
            : lossPercent > 0 ? lossPercent.Value.ToString("#,##0.##") + "%"
            : "0%";

        public static string GetMaxLossLimit(decimal? maxLossLimit)
            => !maxLossLimit.HasValue ? "-"
            : maxLossLimit.Value == 0 ? "Unlimited"
            : maxLossLimit.Value.ToString("#,##0.##");

        public static string GetRemainingBuyInAmount(decimal? remainingBuyInAmount, decimal? maxLossLimit)
            => maxLossLimit.HasValue && maxLossLimit.Value == 0 ? "Unlimited"
            : remainingBuyInAmount.HasValue ? remainingBuyInAmount.Value.ToString("#,##0.##") : "-";
    }
}