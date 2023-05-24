namespace Alpha.MemberInfo.Site.ViewModels.ProblemAccount
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Enumerations;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Requests.ProblemAccount;

    public class UpdateMultipleSuspendViewModel
    {
        private const int numberOfDisplayDownlines = 10;

        public UpdateMultipleSuspendViewModel(UpdateMultipleSuspendProblemAccountRequest message)
        {
            RoleId = message.RoleId;
            UserNames = message.UserNames;
            SelectedDownlineUsernames = message.UserNames.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            DisplayShowAllAccountOption = SelectedDownlineUsernames.Count() > numberOfDisplayDownlines;
            TopDownlineUsernames = SelectedDownlineUsernames.Take(numberOfDisplayDownlines);
            MoreDownlineUsernames = SelectedDownlineUsernames.Skip(numberOfDisplayDownlines);
        }

        public bool DisplayShowAllAccountOption { get; }

        public IEnumerable<string> TopDownlineUsernames { get; }

        public IEnumerable<string> SelectedDownlineUsernames { get; }

        public IEnumerable<string> MoreDownlineUsernames { get; }

        public string Ids { get; }
        public string UserNames { get; }

        public byte RoleId { get; }

        public UserLevel Role => Enumeration.FromValue<UserLevel>(RoleId);

        public string ConfirmMessage => $"{Resources.SelectedAccounts} {string.Join(", ", TopDownlineUsernames)}";

        public int TotalAllAccount => SelectedDownlineUsernames.Count();

    }
}