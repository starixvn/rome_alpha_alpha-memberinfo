using Alpha.MemberInfo.Models;

namespace Alpha.MemberInfo.Site.ViewModels.Transfer
{
    public class CashBalanceTransferViewModel
    {
        public CashBalanceTransferViewModel(
            decimal uplineCashBalance,
            string userName,
            decimal cashBalance,
            decimal outstanding,
            decimal givenCredit,
            UserLevel uplineUserLevel)
        {
            UplineCashBalance = uplineCashBalance;
            UserName = userName;
            CashBalance = cashBalance;
            Outstanding = outstanding;
            GivenCredit = givenCredit;
            UplineUserLevel = uplineUserLevel;
        }

        public decimal UplineCashBalance { get; }

        public UserLevel UplineUserLevel { get; }

        public string UserName { get; }

        public decimal CashBalance { get; }

        public decimal Outstanding { get; }

        public decimal GivenCredit { get; }

        public decimal TransferAmountFrom
        {
            get
            {
                if (CashBalance > 0)
                {
                    return -(CashBalance - Outstanding);
                }

                return 0;
            }
        }

        public decimal TransferAmountTo
        {
            get
            {
                return UplineCashBalance > 0 ? UplineCashBalance : 0;
            }
        }
    }
}