namespace Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.ViewModels
{
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
            DateRangePicker = new DateRangePickerJQueryViewModel(
                Resources.AddedDate,
                request.Language,
                request.FromDate,
                request.ToDate);
        }

        public int SearchAccountId { get; }

        public GlobalThresholdModel GlobalThresholdModel { get; }

        public bool IsMobile { get; }

        public MyWatchlistAccountsPagingViewModel MyWatchListAccountPagingViewModel { get; }

        public bool IsShowLastLoginColumn { get; }

        public string CustLevel { get; }

        public bool CanUpdateAccount { get; }

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
                yield return new SelectListItem { Text = Resources.All, Value = "2", Selected = true };
                yield return new SelectListItem { Text = Resources.Yes, Value = "1" };
                yield return new SelectListItem { Text = Resources.No2, Value = "0" };
            }
        }

        public string UserGuideLink
        {
            get
            {
                if (AppSettings.SupportedNewHandbook)
                {
                    return MicroSites.HowToUseUrl($"~/Default/GetBySlug?slug=my-watchlist");
                }

                return AppSettings.IsSiteLabel789y ?
                    MicroSites.MessageUrl("~/StaticMessage/Guide?id=179067") :
                    MicroSites.HowToUseUrl("~/Index.aspx?linktomessagesite=179067");
            }
        }
    }
}