namespace Alpha.MemberInfo.Site.ViewModels.ProblemAccount
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Alpha.MemberInfo.Constants;
    using Alpha.MemberInfo.Models.ProblemAccount;
    using Alpha.MemberInfo.Site.Requests.ProblemAccount;
    using Alpha.MemberInfo.Site.ViewModels.Components;
    using Alpha.Web.Helpers;

    public class ProblemAccountReportViewModel
    {
        public ProblemAccountReportViewModel(
            IndexRequest query,
            IEnumerable<ProblemAccountModel> models)
        {
            DateRangePickerViewModel = new DateRangePickerViewModel(new DateTime(2020, 1, 1), DateTime.Now, query.FromDate, query.ToDate);
            var status = ReviewStatusConstant.List
                .Select(s => new SelectListItem
                {
                    Text = AppResources.Resources.ResourceManager.GetString(s.Value),
                    Value = s.Key.ToString(),
                    Selected = s.Key == query.UserSelectedReviewedStatusId
                });
            ReviewedStatusSelectionViewModel = new ReviewedStatusSelectionViewModel(status, query.UserSelectedReviewedStatusId);
            Models = models;
            Query = query;
        }

        public ReviewedStatusSelectionViewModel ReviewedStatusSelectionViewModel { get; }

        public DateRangePickerViewModel DateRangePickerViewModel { get; }

        public IEnumerable<ProblemAccountModel> Models { get; set; }

        public IndexRequest Query { get; }

        public const int TotalColumnMaster = 10;

        public const int TotalColumnAgent = 9;

        public const int TotalColumnMember = 8;

        public static string UsernameAutoSuggestionPath
            => MicroSites.OldMainUrl("~/_GlobalResources/Handlers/QueryUserName.ashx");
    }
}
