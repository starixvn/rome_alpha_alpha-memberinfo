namespace Alpha.MemberInfo.Site.Features.ParlayMaxPayout.ViewModels
{
    using Alpha.MemberInfo.Extensions;
    using Alpha.MemberInfo.Features.ParlayMaxPayout.Models;
    using Alpha.MemberInfo.Services;
    using Fanex.RemotingRef;

    public class ParlayMaxPayoutViewModel
    {
        private readonly CacheRefService cacheRefService
            = new CacheRefServiceImpl(new RemotingRefService());

        public ParlayMaxPayoutViewModel(ParlayMaxPayoutModel model)
        {
            BetTypeId = model.BetTypeId;
            SportTypeId = model.SportTypeId;

            BetTypeName = cacheRefService.GetBetTypeName(BetTypeId);
            SportTypeName = cacheRefService.GetSportTypeName(SportTypeId);

            Combo2 = model.Combo2.ToMoneyFormat();
            UplineCombo2 = model.UplineCombo2.ToMoneyFormat();

            Combo35 = model.Combo35.ToMoneyFormat();
            UplineCombo35 = model.UplineCombo35.ToMoneyFormat();

            ComboOver6 = model.ComboOver6.ToMoneyFormat();
            UplineComboOver6 = model.UplineComboOver6.ToMoneyFormat();

            SingleMatchParlay = model.SingleMatchParlay.ToMoneyFormat();
            UplineSingleMatchParlay = model.UplineSingleMatchParlay.ToMoneyFormat();

            IsAllowUnlimitCombo2 = model.IsAllowUnlimitCombo2;
            IsAllowUnlimitCombo35 = model.IsAllowUnlimitCombo35;
            IsAllowUnlimitComboOver6 = model.IsAllowUnlimitComboOver6;
            IsAllowUnlimitSingleMatchParlay = model.IsAllowUnlimitSingleMatchParlay;
        }

        public byte BetTypeId { get; }

        public string BetTypeName { get; }

        public byte SportTypeId { get; }

        public string SportTypeName { get; }

        public string SingleMatchParlay { get; }

        public string Combo2 { get; }

        public string Combo35 { get; }

        public string ComboOver6 { get; }

        public string UplineSingleMatchParlay { get; }

        public string UplineCombo2 { get; }

        public string UplineCombo35 { get; }

        public string UplineComboOver6 { get; }

        public bool IsAllowUnlimitSingleMatchParlay { get; }

        public bool IsAllowUnlimitCombo2 { get; }

        public bool IsAllowUnlimitCombo35 { get; }

        public bool IsAllowUnlimitComboOver6 { get; }
    }
}