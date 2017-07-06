namespace Alpha.MemberInfo.Site.ViewModels.Transfer
{
    public class HintTransferViewModel
    {
        public HintTransferViewModel(string allowTransfer, bool showWarningTransfer)
        {
            AllowTransfer = allowTransfer;

            if (showWarningTransfer)
            {
                WarningTransfer = AppResources.Transfer.WarningMultipleTransfer;
            }
        }

        public string AllowTransfer { get; }

        public string WarningTransfer { get; }
    }
}