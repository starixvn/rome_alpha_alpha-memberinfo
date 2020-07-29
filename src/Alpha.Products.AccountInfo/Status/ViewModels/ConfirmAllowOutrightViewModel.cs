namespace Alpha.Products.AccountInfo.Status.ViewModels
{
    using Alpha.Products.AccountInfo.Requests;

    public class ConfirmAllowOutrightViewModel : ConfirmStatusItemViewModelBase
    {
        public ConfirmAllowOutrightViewModel(ISearchAccountInfoRequest request, int displayOrder = 3)
        : base(request, displayOrder)
        {
        }

        public override bool Disabled
            => base.Disabled || !Request.GetData<bool>(DataKey.SearchAccount.Upline.IsAllowedOutright);

        public string Tooltip => Disabled ? Request.GetResource("OutrightDisabledUpline") : string.Empty;

        public override bool Checked => Request.GetData<bool>(DataKey.SearchAccount.IsAllowedOutright);

        internal override string SuffixMessage => Checked ? "InCaseDisabledMessage" : "InCaseEnabledMessage";

        internal override string StatusName => DataKey.StatusMapping.DisableOutright;

        internal override bool StatusValue => Checked; 
    }
}