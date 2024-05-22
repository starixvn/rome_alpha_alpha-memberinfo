namespace Alpha.MemberInfo.Site.Features.MemberBettingBehavior.ViewModels
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class MemberBettingBehaviorViewModel
    {
        public MemberBettingBehaviorViewModel() { }

        public IEnumerable<SelectListItem> DateRange => new[] { 
            new SelectListItem { Text = "Last 90 days", Value = "90", Selected = true },
            new SelectListItem { Text = "Last 60 days", Value = "60"},
            new SelectListItem { Text = "Last 30 days", Value = "30" },
        };
    }
}