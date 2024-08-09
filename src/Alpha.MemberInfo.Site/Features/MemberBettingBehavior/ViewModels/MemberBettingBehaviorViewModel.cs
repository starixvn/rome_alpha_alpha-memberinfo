namespace Alpha.MemberInfo.Site.Features.MemberBettingBehavior.ViewModels
{
    using Alpha.MemberInfo.Constants;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;


    public class MemberBettingBehaviorViewModel
    {
        public MemberBettingBehaviorViewModel(bool isDarkMode) {
            IsDarkMode = isDarkMode;
        }

        public IEnumerable<SelectListItem> DateRange
        {
            get
            {
                foreach(var item in MemberStatisticDateRange.Items)
                {
                    yield return new SelectListItem { Text = AppResources.Resources.ResourceManager.GetString(item.Key), Value = item.Value.ToString() };
                }
            }
        }

        public bool IsDarkMode { get; }

        public DateTime EndDate => DateTime.Now.AddDays(-2);
    }
}