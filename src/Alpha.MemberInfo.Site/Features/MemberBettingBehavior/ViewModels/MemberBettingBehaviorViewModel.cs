namespace Alpha.MemberInfo.Site.Features.MemberBettingBehavior.ViewModels
{
    using Alpha.MemberInfo.Constants;
    using System.Collections.Generic;
    using System.Web.Mvc;


    public class MemberBettingBehaviorViewModel
    {
        public MemberBettingBehaviorViewModel() { }

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
    }

}