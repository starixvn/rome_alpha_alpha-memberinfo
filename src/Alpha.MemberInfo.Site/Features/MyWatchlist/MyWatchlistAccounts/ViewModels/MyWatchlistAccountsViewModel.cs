namespace Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.ViewModels
{
    using Alpha.MemberInfo.Configurations;
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.Requests;
    using Alpha.Web.Helpers;
    using Fanex.Common.Extensions;
    using Sunplus.Agent.Model.Entities;
    using System;

    public class MyWatchlistAccountsViewModel
    {
        public MyWatchlistAccountsViewModel(
            MyWatchlistAccountsQuery request,
            GlobalThresholdModel globalThresholdModel,
            bool isMobile,
            bool canUpdateAccount,
            MyWatchlistAccountsPagingViewModel myWatchListAccountPagingViewModel = null)
        {
            SearchAccountId = request.CustId;
            IsMobile = isMobile;
            CanUpdateAccount = canUpdateAccount;
            GlobalThresholdModel = globalThresholdModel;
            MyWatchListAccountPagingViewModel = myWatchListAccountPagingViewModel;
            IsShowLastLoginColumn = request.IsShowLastLoginColumn;
            CustLevel = request.CustLevel.DisplayName;
        }

        public int SearchAccountId { get; }

        public GlobalThresholdModel GlobalThresholdModel { get; }

        public bool IsMobile { get; }

        public MyWatchlistAccountsPagingViewModel MyWatchListAccountPagingViewModel { get; }

        public bool IsShowLastLoginColumn { get; }

        public string CustLevel { get; }

        public bool CanUpdateAccount { get; }

        public DateTime MinDate => DateTime.Today.AddMonths(-3).FirstDayOfMonth();

        public string UserGuideLink => AppSettings.IsSiteLabel789y ?
            MicroSites.MessageUrl("~/StaticMessage/Guide?id=179067") :
            MicroSites.HowToUseUrl("~/Index.aspx?linktomessagesite=179067");
    }
}