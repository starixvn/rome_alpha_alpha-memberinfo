namespace Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Configurations;
    using Alpha.MemberInfo.Constants;
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.Requests;
    using Alpha.MemberInfo.Site.ViewModels.Components;
    using Alpha.Web.Helpers;
    using Fanex.Common.Extensions;

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
            IsOpenFromProblemAccount = request.IsOpenFromProblemAccount;
            CustIds = request.CustIds;
            DateRangePicker = new DateRangePickerJQueryViewModel(
                Resources.AddedDate,
                request.Language,
                request.FromDate,
                request.ToDate,
                DateTime.Today.AddMonths(-3).FirstDayOfMonth());
        }

        public int SearchAccountId { get; }

        public GlobalThresholdModel GlobalThresholdModel { get; }

        public bool IsMobile { get; }

        public MyWatchlistAccountsPagingViewModel MyWatchListAccountPagingViewModel { get; }

        public bool IsShowLastLoginColumn { get; }

        public string CustLevel { get; }

        public bool CanUpdateAccount { get; }

        public bool IsOpenFromProblemAccount { get; }

        public string CustIds { get; }

        public DateRangePickerJQueryViewModel DateRangePicker { get; }

        public IEnumerable<SelectListItem> Status
            => ReachedThresholdStatusConstants.Items.Select(s => new SelectListItem
            {
                Text = Resources.ResourceManager.GetString(s.Value),
                Value = s.Key.ToString(),
                Selected = s.Key == ReachedThresholdStatusConstants.All
            });

        public IEnumerable<SelectListItem> ProblemAccounts
        {
            get
            {
                yield return new SelectListItem { Text = Resources.All, Value = "0", Selected = true };
                yield return new SelectListItem { Text = Resources.Yes, Value = "true" };
                yield return new SelectListItem { Text = Resources.No2, Value = "false" };
            }
        }

        public string UserGuideLink => AppSettings.IsSiteLabel789y ?
            MicroSites.MessageUrl("~/StaticMessage/Guide?id=179067") :
            MicroSites.HowToUseUrl("~/Index.aspx?linktomessagesite=179067");
    }
}