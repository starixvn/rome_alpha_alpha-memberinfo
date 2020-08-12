namespace Alpha.Products.AccountInfo.Setting.ViewModels.Products
{
    using System.Collections.Generic;
    using Alpha.Products.AccountInfo.Requests;
    using Base;

    /// <summary>
    ///     View model for all products use DCS
    /// </summary>
    public class DynamicProductMultipleViewModel : DynamicProductViewModel, ISettingItemMultipleViewModel
    {
        public DynamicProductMultipleViewModel(ISearchAccountInfoRequest request, int productId)
            : base(request, productId)
        {
        }

        public override string Text => $"Edit Multiple {base.Text}";

        public override string Icon => "icon-edit Enable";

        protected override Dictionary<string, string> QueryStringParamaters
        {
            get
            {
                var query = new Dictionary<string, string>
                {
                    ["isGetMultipleDCS"] = "1",
                    ["isMultiple"] = "true",
                    ["productId"] = ProductId.ToString(),
                    ["uplineId"] = Actor.Id.ToString(),
                    ["actorId"] = Actor.Id.ToString(),
                    ["customerIds"] = Request.GetData<string>("customerIds"),
                    ["usernames"] = Request.GetData<string>("usernames"),
                    ["customerNames"] = Request.GetData<string>("usernames"),
                    ["customerLevel"] = SearchAccount.CusomerLevelValue.ToString(),
                    ["subAccId"] = Actor.SubAccountId.ToString(),
                    ["formType"] = "1",
                    ["isForcingMode"] = "false"
                };

                if (SearchAccount.IsMember)
                {
                    query.Add("uplinelevel", Actor.CustomerLevel.Value.ToString());
                }

                return query;
            }
        }
    }
}