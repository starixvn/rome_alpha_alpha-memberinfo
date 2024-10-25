namespace Alpha.MemberInfo.Site.Features.MemberBettingBehavior.ViewModels
{
    using Alpha.MemberInfo.Configurations;
    using Alpha.MemberInfo.Constants;
    using Alpha.Web.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class MemberBettingBehaviorViewModel
    {
        public MemberBettingBehaviorViewModel(bool isDarkMode, string language)
        {
            IsDarkMode = isDarkMode;
            Language = language;
        }

        public IEnumerable<SelectListItem> DateRange
        {
            get
            {
                foreach (var item in MemberStatisticDateRange.Items)
                {
                    yield return new SelectListItem { Text = AppResources.Resources.ResourceManager.GetString(item.Key), Value = item.Value.ToString() };
                }
            }
        }

        public bool IsDarkMode { get; }

        public DateTime EndDate => DateTime.Now.AddDays(-2);

        public string UserGuideLink
        {
            get
            {
                if (AppSettings.SupportedNewHandbook)
                {
                    return MicroSites.HowToUseUrl($"~/Default/GetBySlug?slug=member-betting-behavior");
                }

                return AppSettings.IsMBCSite
                   ? MicroSites.HowToUseUrl("~/Index.aspx?linktomessagesite=204089")
                   : MicroSites.MessageUrl("~/StaticMessage/Guide?id=204089");
            }
        }

        public string Language { get; }
    }
}