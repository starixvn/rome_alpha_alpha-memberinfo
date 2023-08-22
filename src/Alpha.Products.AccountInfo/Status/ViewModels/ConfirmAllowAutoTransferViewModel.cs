namespace Alpha.Products.AccountInfo.Status.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Web.Configurations;
    using Alpha.Web.Helpers;

    public class ConfirmAllowAutoTransferViewModel : ConfirmStatusItemViewModelBase
    {
        public ConfirmAllowAutoTransferViewModel(ISearchAccountInfoRequest request, int displayOrder = 7)
           : base(request, displayOrder)
        {
        }

        public override bool Visible => Request.SupportAutoTransfer;

        public override bool Disabled
                => base.Disabled || Request.ActorIsNotEdittingDirectDownline;

        public override bool Checked => Request.GetData<bool>(DataKey.SearchAccount.IsAllowAutoTransfer);

        public override bool ReloadCustomerList => true;

        public override bool ShowInCustomerList => true;

        internal override string SuffixMessage
        {
            get
            {
                return Checked ? "InCaseDisabledMemberMessage" : "InCaseEnabledMemberMessage";
            }
        }

        internal override string StatusName => DataKey.StatusMapping.AutoTransfer;

        internal override bool StatusValue => !Checked;

        public override string Url => MicroSites.TransferUrl("autotransfer/update");

        internal new IDictionary<string, object> Params =>
            new Dictionary<string, object>
                    {
                        {"CustId", SearchAccount.Id.ToString()},
                        {"CustName", SearchAccount.Username.ToString()},
                        {"State", StatusValue? "on" : "off"},
                        {"UseDifferentSite", 1}
                    };

        public new string JsonParams =>
            string.Format("{0}{1}{2}", "{", string.Join(",", Params.Select(x => $"\"{x.Key}\":\"{x.Value.ToString()}\"")), "}");
    }
}