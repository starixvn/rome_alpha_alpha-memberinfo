namespace Alpha.Products.AccountInfo.Status.ViewModels
{
    using Alpha.Products.AccountInfo.Requests;

    public class ConfirmUnlockAccessViewModel : ConfirmStatusItemViewModelBase
    {
        public ConfirmUnlockAccessViewModel(ISearchAccountInfoRequest request, int displayOrder = 3)
            : base(request, displayOrder)
        {
        }

        public override bool Disabled => base.Disabled || !Checked;

        public override bool Checked => Request.GetData<byte>(DataKey.SearchAccount.IsLockedAccess) != DataKey.LockedAccess.None;

        public override bool ReloadCustomerInfo => true;

        public override bool ReloadCustomerList => true;

        public override bool ShowInCustomerList => false;

        internal override string SuffixMessage
        {
            get
            {
                var locked = Request.GetData<byte>(DataKey.SearchAccount.IsLockedAccess);

                if (locked == DataKey.LockedAccess.FalseInputOTPSC)
                {
                    return "InCaseFalseInputOTPSC";
                }

                if (locked == DataKey.LockedAccess.FalseInputPassword)
                {
                    return "InCaseFalseInputPassword";
                }

                return "InCaseNone";
            }
        }

        internal override string StatusName => DataKey.StatusMapping.Unlock;

        internal override bool StatusValue => !Checked;
    }
}
