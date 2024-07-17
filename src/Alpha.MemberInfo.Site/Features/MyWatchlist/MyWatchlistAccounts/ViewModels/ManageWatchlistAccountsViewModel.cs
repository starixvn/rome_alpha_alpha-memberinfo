namespace Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Alpha.MemberInfo.Extensions;
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.Requests;

    public class ManageWatchlistAccountsViewModel
    {
        public ManageWatchlistAccountsViewModel(
            ManageWatchlistAccountsQuery message,
            IEnumerable<PersonalPresetModel> presets,
            ThresholdViewModel threshold)
        {
            PresetSelectionViewModel = new PresetSelectionViewModel(
                presets.Select(preset => new SelectListItem
                {
                    Text = $"{preset.PresetTitle} ({preset.WinningThreshold.ToMoneyFormat()} - {preset.LosingThreshold.ToMoneyFormat()})",
                    Value = $"{preset.WinningThreshold.ToMoneyFormat()} - {preset.LosingThreshold.ToMoneyFormat()} - {preset.PresetTitle}",
                    Selected = false
                }), 1);
            Threshold = threshold;
            CustIds = message.CustIds;
            Usernames = message.Usernames;
            IsEditWatchlistAccountsPopup = message.IsEdit;
            MyWatchlistPage = message.MyWatchlistPage;
        }

        public PresetSelectionViewModel PresetSelectionViewModel { get; }

        public ThresholdViewModel Threshold { get; }

        public string CustIds { get; }

        public string Usernames { get; }

        public bool IsEditWatchlistAccountsPopup { get; }

        public bool MyWatchlistPage { get; }
    }
}