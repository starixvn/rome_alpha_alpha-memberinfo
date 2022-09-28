namespace Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.ViewModels
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class PresetSelectionViewModel
    {
        public PresetSelectionViewModel(IEnumerable<SelectListItem> presets, int userSelectedId)
        {
            Presets = presets;
            UserSelectedStatusId = userSelectedId;
        }

        public IEnumerable<SelectListItem> Presets { get; }

        public int UserSelectedStatusId { get; }
    }
}