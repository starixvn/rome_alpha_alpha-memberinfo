namespace Alpha.MemberInfo.Site.Features.ParlayMaxPayout.ViewModels
{
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Constants;
    using Alpha.MemberInfo.Features.ParlayMaxPayout.Models;
    using System.Collections.Generic;

    public class ParlayMaxPayoutBasePageViewModel
    {
        private readonly Dictionary<byte, string> classNameDictionary
            = new Dictionary<byte, string>
            {
                [SportTypeConstants.SoccerParlay] = "soccer-parlay",
                [SportTypeConstants.MixedSportsParlay] = "mixed-sports-parlay"
            };

        private const string parlayMaxPayoutSettingPartialViewPath
            = "~/Features/ParlayMaxPayout/Views/ParlayMaxPayoutSetting.cshtml";

        private readonly IEnumerable<ParlayMaxPayoutModel> data;

        public ParlayMaxPayoutBasePageViewModel(
            IEnumerable<ParlayMaxPayoutModel> data)
        {
            this.data = data;

            Combo2FieldText = Resources.Combo2;
            Combo35FieldText = Resources.Combo35;
            ComboOver6FieldText = Resources.ComboOver6;
            SingleMatchParlayFieldText = Resources.SingleMatchParlay;
            ParlayMaxPayoutSettingPartialViewPath = parlayMaxPayoutSettingPartialViewPath;
        }

        public virtual string SubTitle { get; }

        public virtual string SubText { get; }

        public string Combo2FieldText { get; }

        public string Combo35FieldText { get; }

        public string ComboOver6FieldText { get; }

        public string SingleMatchParlayFieldText { get; }

        public IEnumerable<ParlayMaxPayoutViewModel> Data
        {
            get
            {
                foreach (var model in data)
                {
                    yield return new ParlayMaxPayoutViewModel(model);
                }
            }
        }

        public string ParlayMaxPayoutSettingPartialViewPath { get; }

        public string GetClassName(byte sportTypeId) => classNameDictionary[sportTypeId];
    }
}