namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.ReachedThreshold.ViewModels
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class StatusSelectionViewModel
    {
        public StatusSelectionViewModel(IEnumerable<SelectListItem> statuses, int userSelectedId)
        {
            Statuses = statuses;
            UserSelectedStatusId = userSelectedId;
        }

        public IEnumerable<SelectListItem> Statuses { get; }

        public int UserSelectedStatusId { get; }
    }
}