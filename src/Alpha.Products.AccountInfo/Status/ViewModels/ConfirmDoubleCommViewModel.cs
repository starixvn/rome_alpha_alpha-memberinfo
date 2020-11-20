namespace Alpha.Products.AccountInfo.Status.ViewModels
{
    using Alpha.Products.AccountInfo.Requests;

    public class ConfirmDoubleCommViewModel : ConfirmStatusItemViewModelBase
    {
        public ConfirmDoubleCommViewModel(ISearchAccountInfoRequest request, int displayOrder = 5)
           : base(request, displayOrder)
        {
        }

        public override bool Disabled
            => base.Disabled
                || !Request.GetData<bool>(DataKey.SearchAccount.Upline.IsAllowedDoubleComm)
                || Request.ActorIsNotEdittingDirectDownline;

        public override bool Checked => Request.GetData<bool>(DataKey.SearchAccount.IsAllowedDoubleComm);

        internal override string SuffixMessage
        {
            get
            {
                if (Request.SearchAccount.IsMember)
                {
                    return Checked ? "InCaseDisabledMemberMessage" : "InCaseEnabledMemberMessage";
                }

                return Checked ? "InCaseDisabledMasterOrAgentMessage" : "InCaseEnableMasterOrAgentMessage";
            }
        }

        internal override string StatusName => DataKey.StatusMapping.DoubleComm;

        internal override bool StatusValue => !Checked;
    }
}