using Alpha.MemberInfo.Models;

namespace Alpha.MemberInfo.Site.ViewModels.Transfer
{
    public class CashBalanceTransferViewModel
    {
        public CashBalanceTransferViewModel(
            decimal uplineCashBalance,
            int custId,
            string userName,
            UserLevel custUserLevel,
            decimal cashBalance,
            decimal outstanding,
            decimal givenCredit)
        {
            UplineCashBalance = uplineCashBalance;
            CustId = custId;
            UserName = userName;
            CustUserLevel = custUserLevel;
            CashBalance = cashBalance;
            Outstanding = outstanding;
            GivenCredit = givenCredit;
        }

        public decimal UplineCashBalance { get; }

        public int CustId { get; }

        public string UserName { get; }

        public UserLevel CustUserLevel { get; }

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

        public string Note
        {
            get
            {
                string amountFrom = TransferAmountFrom == 0 ? "0" : TransferAmountFrom.ToString($"#,##0.0000");
                string amountTo = TransferAmountTo == 0 ? "0" : TransferAmountTo.ToString($"#,##0.0000");

                return string.Format(
                    AppResources.Transfer.AvailableRangeTransfer,
                    amountFrom,
                    amountTo);
            }
        }
    }
}