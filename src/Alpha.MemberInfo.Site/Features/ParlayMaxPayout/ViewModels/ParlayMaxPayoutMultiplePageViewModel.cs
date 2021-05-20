namespace Alpha.MemberInfo.Site.Features.ParlayMaxPayout.ViewModels
{
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Features.ParlayMaxPayout.Models;
    using Requests;
    using System.Collections.Generic;

    public class ParlayMaxPayoutMultiplePageViewModel : ParlayMaxPayoutBasePageViewModel
    {
        private readonly ParlayMaxPayoutMultipleSelectQuery message;

        public ParlayMaxPayoutMultiplePageViewModel(
            ParlayMaxPayoutMultipleSelectQuery message,
            IEnumerable<ParlayMaxPayoutModel> models) : base(models)
        {
            this.message = message;
        }

        public override string SubTitle
            => string.Join(", ", message.CustNames.Split(','));

        public override string SubText => Resources.ParlayMaxPayoutMultipleSubText;
    }
}