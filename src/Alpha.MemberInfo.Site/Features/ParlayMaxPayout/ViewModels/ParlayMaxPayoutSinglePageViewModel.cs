namespace Alpha.MemberInfo.Site.Features.ParlayMaxPayout.ViewModels
{
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Features.ParlayMaxPayout.Models;
    using Requests;
    using System.Collections.Generic;

    public class ParlayMaxPayoutSinglePageViewModel : ParlayMaxPayoutBasePageViewModel
    {
        private readonly ParlayMaxPayoutSingleSelectQuery message;

        public ParlayMaxPayoutSinglePageViewModel(
            ParlayMaxPayoutSingleSelectQuery message,
            IEnumerable<ParlayMaxPayoutModel> models) : base(models)
        {
            this.message = message;
        }

        public override string SubTitle
            => string.Format(Resources.ParlayMaxPayoutSubTitle, message.CustName);

        public override string SubText
            => string.Format(Resources.ParlayMaxPayoutSingleSubText, message.CustName);
    }
}