namespace Alpha.Products.AccountInfo.Setting.ViewModels.Products
{
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.Core.Configuration;
    using Base;
    using System.Collections.Generic;

    public class DynamicProductMultipleViewModel : DynamicProductViewModel, ISettingItemMultipleViewModel
    {
        public DynamicProductMultipleViewModel(ISearchAccountInfoRequest request, IConfigurationItem productItem)
            : base(request, productItem)
        {
        }

        public override string Text => $"{Request.GetResource("EditMultiple")} {Request.GetProductName(productItem.ProductId)}";

        public override string Icon => "icon-edit Enable";

        public override string Url => base.BaseAddress;

        public override Dictionary<string, string> QueryStringParamaters
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
                    ["isForcingMode"] = "false",
                    ["language"] = "E"
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