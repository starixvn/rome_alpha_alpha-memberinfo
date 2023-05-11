namespace Alpha.MemberInfo.Site.ViewModels.ProblemAccount
{
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models;
    using System.Linq;
    
    public class ConfirmAddToWatchlistViewModel
    {
        private const int limitMember = 50;

        public ConfirmAddToWatchlistViewModel(MultipleWatchlistAccountModel multipleWatchlistAccountModel)
        {
            var alreadyAddedAccounts = multipleWatchlistAccountModel.AlreadyAddedAccounts;
            var newlyAddedAccounts = multipleWatchlistAccountModel.NewlyAddedAccounts;
            if (alreadyAddedAccounts.Any())
            {
                AlreadyAddedAccounts = string.Join(", ", alreadyAddedAccounts.Select(c => c.Username).ToArray());
            }
            if (newlyAddedAccounts.Any())
            {
                if (newlyAddedAccounts.Count() <= limitMember)
                {
                    NewlyAddedAccounts = string.Join(", ", newlyAddedAccounts.Select(c => c.Username).ToArray());
                }
                else
                {
                    IsExceedLimit = true;
                    NewlyAddedAccounts = string.Join(", ", newlyAddedAccounts.Take(limitMember).Select(c => c.Username).ToArray());
                }
            }
        }

        public string AlreadyAddedAccounts { get; }

        public string NewlyAddedAccounts { get; }

        public bool HasAlreadyAddedAccounts => !string.IsNullOrWhiteSpace(AlreadyAddedAccounts);

        public bool HasNewlyAddedAccounts => !string.IsNullOrWhiteSpace(NewlyAddedAccounts);

        public bool IsExceedLimit { get; }
    }
}
