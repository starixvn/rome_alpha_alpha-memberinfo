namespace Alpha.Products.AccountInfo.Status.ViewModels
{
    using Alpha.Products.AccountInfo.Requests;

    public class ConfirmClosedViewModel : ConfirmStatusItemViewModelBase
    {
        public ConfirmClosedViewModel(ISearchAccountInfoRequest request, int displayOrder = 2)
            : base(request, displayOrder)
        {
        }

        public override bool Disabled => base.Disabled || Request.GetData<bool>(DataKey.SearchAccount.Upline.IsClosed);

        private bool IsClosedSearchAccount => Request.GetData<bool>(DataKey.SearchAccount.IsClosed);

        public override bool Checked => IsClosedSearchAccount;

        public override bool ReloadCustomerInfo => true;

        public override bool ReloadCustomerList => true;

        public override bool ShowInCustomerList => true;

        internal override string SuffixMessage =>
            !Request.SearchAccount.IsMember && !Checked ?
                    "InCaseClosedDownlinesMessage" : "InCaseChangeClosedStatusMessage";

        internal override string StatusName => DataKey.StatusMapping.Closed;

        internal override bool StatusValue => !Checked;

        public override bool IsPopup => IsClosedSearchAccount && SearchAccount.IsNotMember;

        public override string PopupHeaderText => IsPopup ? Request.GetResource("OpenCustomerPopupHeaderText") : base.PopupHeaderText;

        public override string Url => IsPopup
            ? $"{AccountInfoUrl}OpenDownlines/EditSingle?SearchAccountId={SearchAccount.Id}&SearchAccountName={SearchAccount.Username}"
            : base.Url;
    }
}