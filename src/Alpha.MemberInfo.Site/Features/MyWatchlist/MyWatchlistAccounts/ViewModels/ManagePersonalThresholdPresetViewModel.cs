using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models;
using Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.Requests;
using System.Collections.Generic;

namespace Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.ViewModels
{
    public class ManagePersonalThresholdPresetViewModel
    {
        public ManagePersonalThresholdPresetViewModel(IEnumerable<PersonalPresetModel> personalPresets)
        {
            PersonalPresets = personalPresets;
        }

        public IEnumerable<PersonalPresetModel> PersonalPresets { get; }
    }
}