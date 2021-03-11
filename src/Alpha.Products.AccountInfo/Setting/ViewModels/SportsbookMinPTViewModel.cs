namespace Alpha.Products.AccountInfo.Setting.ViewModels
{
    using System.Collections.Generic;
    using Alpha.Products.AccountInfo.Requests;
    using Base;

    public class SportsbookMinPTViewModel : SettingItemViewModel
    {
        public SportsbookMinPTViewModel(ISearchAccountInfoRequest request, int displayOrder = 5)
            : base(request, displayOrder)
        {
        }

        protected override string BaseAddress
            => $"{OldMainUrl}_MemberInfo/PositionTaking/{Actor.CustomerLevel.DisplayName}/EditMinPT{SearchAccount.CustomerLevel.DisplayName}.aspx";

        public override Dictionary<string, string> QueryStringParamaters
            => new Dictionary<string, string>
            {
                ["custId"] = SearchAccount.Id.ToString(),
                ["username"] = SearchAccount.Username
            };

        public override bool Visible => base.Visible && SearchAccount.IsNotMember;
    }
}