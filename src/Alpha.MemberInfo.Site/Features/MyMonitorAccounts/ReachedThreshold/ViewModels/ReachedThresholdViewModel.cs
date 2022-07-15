namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.ReachedThreshold.ViewModels
{
    using Alpha.MemberInfo.Constants;
    using Alpha.MemberInfo.Features.MyMonitorAccounts.MyWatchlistAccounts.Models;
    using Alpha.MemberInfo.Site.Features.MyMonitorAccounts.ReachedThreshold.Requests;
    using Alpha.MemberInfo.Site.ViewModels.Components;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class ReachedThresholdViewModel
    {
        public ReachedThresholdViewModel(
            ReachedThresholdRequest request,
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
            LimitedDateToShowInfo = request.LimitedDateToShowInfo;
            CustLevel = request.CustLevel.DisplayName;
        }

        public ReachedThresholdPagingViewModel ReachedThresholdPagingViewModel { get; }

        public IEnumerable<SelectListItem> StatusOptions { get; }

        public IEnumerable<SelectListItem> LimitTypeOptions { get; }

        public DateRangePickerViewModel DateRangePickerViewModel { get; }

        public GlobalThresholdModel GlobalThresholdModel { get; }

        public int SearchAccountId { get; }

        public bool IsMobile { get; }

        public bool LimitedDateToShowInfo { get; }

        public string CustLevel { get; }
    }
}