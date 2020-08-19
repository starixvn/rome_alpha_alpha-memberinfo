namespace Alpha.Products.AccountInfo.Setting.ViewModels
{
    using System.Collections.Generic;
    using Alpha.Products.AccountInfo.Requests;
    using Base;

    public class RemoveOtpViewModel : SettingItemViewModel
    {
        public RemoveOtpViewModel(ISearchAccountInfoRequest request, int displayOrder = 99999)
            : base(request, displayOrder)

        {
        }

        protected override string BaseAddress => $"{MainUrl}Otp/ResetOtp";

        public override Dictionary<string, string> QueryStringParamaters
           => new Dictionary<string, string>
           {
               ["custId"] = SearchAccount.Id.ToString(),
               ["username"] = SearchAccount.Username
           };

        public override bool Visible =>
            base.Visible &&
            SearchAccount.IsNotMember &&
            Request.GetData<bool>(DataKey.Actor.HasUpdateAccountPermission);

        public override string Width => "700px";

        public override string Height => "330px";
    }
}