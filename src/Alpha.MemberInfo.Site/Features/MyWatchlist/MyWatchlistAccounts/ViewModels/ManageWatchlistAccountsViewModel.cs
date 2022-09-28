namespace Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.ViewModels
{
    using Alpha.MemberInfo.Extensions;
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.Requests;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class ManageWatchlistAccountsViewModel
    {
        public ManageWatchlistAccountsViewModel(
            ManageWatchlistAccountsQuery message, 
            IEnumerable<PersonalPresetModel> presets)
        {
            PresetSelectionViewModel = new PresetSelectionViewModel(
                presets.Select(preset => new SelectListItem
                {
                    Text = $"{preset.PresetTitle} ({preset.WinningThreshold.ToMoneyFormat()} - {preset.LosingThreshold.ToMoneyFormat()})",
                    Value = $"{preset.WinningThreshold.ToMoneyFormat()} - {preset.LosingThreshold.ToMoneyFormat()} - {preset.PresetTitle}",
                    Selected = false
                }),
                1);

            IsEditWatchlistAccountsPopup = message.IsEdit;
            if (IsEditWatchlistAccountsPopup)
            {
                CustIds = message.CustIds;
                Usernames = message.Usernames;
                Threshold = new ThresholdViewModel(message.WinningThreshold, message.LosingThreshold);
            }
        }

        public PresetSelectionViewModel PresetSelectionViewModel { get; }

        public bool IsEditWatchlistAccountsPopup { get; }

        public string CustIds { get; }

        public string Usernames { get; }

        public ThresholdViewModel Threshold { get; }
    }
}