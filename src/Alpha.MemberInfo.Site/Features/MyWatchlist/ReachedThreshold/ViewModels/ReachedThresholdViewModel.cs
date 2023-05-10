namespace Alpha.MemberInfo.Site.Features.MyWatchlist.ReachedThreshold.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Alpha.MemberInfo.Configurations;
    using Alpha.MemberInfo.Constants;
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.ReachedThreshold.Requests;
    using Alpha.MemberInfo.Site.ViewModels.Components;
    using Alpha.Web.Helpers;

    public class ReachedThresholdViewModel
    {
        public ReachedThresholdViewModel(
            ReachedThresholdQuery request,
            GlobalThresholdModel globalThresholdModel,
            bool isMobile,
            ReachedThresholdPagingViewModel reachedThresholdPagingViewModel = null)
        {
            SearchAccountId = request.CustId;
            DateRangePickerViewModel = new DateRangePickerViewModel(
                request.MinDate,
                DateTime.Now,
                request.FromDate,
                request.ToDate);

            StatusOptions = ReachedThresholdStatusConstants.Items
                            .Select(s => new SelectListItem
                            {
                                Text = AppResources.Resources.ResourceManager.GetString(s.Value),
                                Value = s.Key.ToString(),
                                Selected = s.Key == ReachedThresholdStatusConstants.All
                            });
            LimitTypeOptions = ReachedThresholdLimitTypeConstants.Items
                                .Select(s => new SelectListItem
                                {
                                    Text = AppResources.Resources.ResourceManager.GetString(s.Value),
                                    Value = s.Key.ToString(),
                                    Selected = s.Key == ReachedThresholdLimitTypeConstants.All
                                });

            ReachedThresholdPagingViewModel = reachedThresholdPagingViewModel;
            GlobalThresholdModel = globalThresholdModel;
            IsMobile = isMobile;
            IsShowLastLoginColumn = request.LimitedDateToShowInfo == default(DateTime);
            CustLevel = request.CustLevel.DisplayName;
            CanUpdateAccount = request.CanUpdateAccount;
        }

        public ReachedThresholdPagingViewModel ReachedThresholdPagingViewModel { get; }

        public IEnumerable<SelectListItem> StatusOptions { get; }

        public IEnumerable<SelectListItem> LimitTypeOptions { get; }

        public DateRangePickerViewModel DateRangePickerViewModel { get; }

        public GlobalThresholdModel GlobalThresholdModel { get; }

        public int SearchAccountId { get; }
        public bool IsMobile { get; }

        public bool IsShowLastLoginColumn { get; }

        public string CustLevel { get; }

        public bool CanUpdateAccount { get; }

        public string UserGuideLink => AppSettings.IsSiteLabel789y ?
            MicroSites.MessageUrl("~/StaticMessage/Guide?id=179067") :
            MicroSites.HowToUseUrl("~/Index.aspx?linktomessagesite=179067");
    }
}