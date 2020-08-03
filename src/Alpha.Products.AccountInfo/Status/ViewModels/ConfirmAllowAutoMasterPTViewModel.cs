namespace Alpha.Products.AccountInfo.Status.ViewModels
{
    using Alpha.Products.AccountInfo.Requests;

    public class ConfirmAllowAutoMasterPTViewModel : ConfirmStatusItemViewModelBase
    {
        public ConfirmAllowAutoMasterPTViewModel(ISearchAccountInfoRequest request, int displayOrder = 4)
          : base(request, displayOrder)
        {
        }

        public override bool Visible => base.Visible && Actor.IsSuper && SearchAccount.IsMaster;

        public override bool Checked => Request.GetData<bool>(DataKey.SearchAccount.IsAllowedMasterAutoPT);

        public override bool ShowInCustomerList => true;

        internal override string SuffixMessage => Checked ? "InCaseDisabledMessage" : "InCaseEnabledMessage";

        internal override string StatusName => DataKey.StatusMapping.DisableAutoMasterPT;

        internal override bool StatusValue => Checked;
    }
}