namespace Alpha.Products.AccountInfo.Status.ViewModels
{
    using Alpha.Products.AccountInfo.Requests;

    public class ConfirmTransferBalanceDailyViewModel : ConfirmStatusItemViewModelBase
    {
        public ConfirmTransferBalanceDailyViewModel(ISearchAccountInfoRequest request, int displayOrder = 6)
           : base(request, displayOrder)
        {
        }

        public override bool Checked => Request.GetData<bool>(DataKey.SearchAccount.IsAllDayBalTransfer);

        internal override string SuffixMessage => "Message";

        public override string ConfirmMessage => string.Format(Request.GetResource($"{GetType().Name}{SuffixMessage}"), SearchAccount.Username);

        internal override string StatusName => DataKey.StatusMapping.IsAllDayBalTransfer;

        internal override bool StatusValue => !Checked;

        public override bool Visible => Request.SearchAccount.IsMaster && Request.GetData<bool>(DataKey.SearchAccount.AllowedAllDayBalTransfer);

        public override string Url => $"{AccountInfoUrl}SearchAccount/UpdateAllDayBalanceTransfer";

        public string Tooltip => Request.GetResource("ConfirmTransferBalanceDailyViewModelTooltip");
    }
}