using Alpha.MemberInfo.Features.MyMonitorAccounts.MyWatchlistAccounts.Models;
using Alpha.MemberInfo.Site.Features.MyMonitorAccounts.MyWatchlistAccounts.Requests;
using System.Collections.Generic;

namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.MyWatchlistAccounts.ViewModels
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