namespace Alpha.Products.AccountInfo.Status.ViewModels
{
    using Alpha.Products.AccountInfo.Requests;

    public class ConfirmProtectedViewModel : ConfirmStatusItemViewModelBase
    {
        public ConfirmProtectedViewModel(ISearchAccountInfoRequest request, int displayOrder = 4)
            : base(request, displayOrder)
        {
        }

        public override bool Visible
            => Request.GetData<bool>(DataKey.SearchAccount.IsOfferProtectedMode)
            && Request.ActorIsEdittingDirectDownline
            && Request.SearchAccount.IsNotMember;

        public override bool Checked => Request.GetData<bool>(DataKey.SearchAccount.IsProtectedMode);

        public override bool ReloadCustomerInfo => true;

        public override bool ReloadCustomerList => false;

        public override bool ShowInCustomerList => false;

        internal override string SuffixMessage
            => "InCaseChangeProtectedStatusMessage";

        internal override string StatusName => DataKey.StatusMapping.Protected;

        internal override bool StatusValue => !Checked;
    }
}
