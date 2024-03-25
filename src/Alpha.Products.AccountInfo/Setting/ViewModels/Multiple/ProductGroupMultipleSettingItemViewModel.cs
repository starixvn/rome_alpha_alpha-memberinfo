using System.Collections.Generic;
using Alpha.Products.AccountInfo.Requests;
using Alpha.Products.AccountInfo.Setting.ViewModels.Base;

namespace Alpha.Products.AccountInfo.Setting.ViewModels.Multiple
{
    public class ProductGroupMultipleSettingItemViewModel : ProductGroupSettingItemViewModel, ISettingItemMultipleViewModel
    {
        public ProductGroupMultipleSettingItemViewModel(ISearchAccountInfoRequest request, int productGroupId)
            : base(request, productGroupId)
        {
            switch (productGroupId)
            {
                case 1:
                    ShowGuideLine = true;
                    GuideLineUrl = $"{OldMainUrl}MemberInformation/MultipleEditSportsBook/GuideLine";
                    break;

                default:
                    ShowGuideLine = false;
                    GuideLineUrl = string.Empty;
                    break;
            }
        }

        public override Dictionary<string, string> QueryStringParamaters
       => new Dictionary<string, string>
       {
           ["productGroupId"] = ProductGroupId.ToString(),
           ["roleId"] = SearchAccount.CusomerLevelValue.ToString(),
           ["lng"] = request.Language,
           ["isDarkMode"] = request.IsDarkMode.ToString().ToLowerInvariant(),
           ["custIds"] = request.GetData<string>("customerIds"),
           ["usernames"] = request.GetData<string>("usernames"),
       };

        public override string Text => $"{request.GetResource("EditMultiple")} {request.GetProductGroupName(ProductGroupId)}";

        public override string Icon => "icon-edit Enable";

        protected override string BaseAddress => $"{MemberInfoUrl}Customer/EditMultiple";

        public override string Url => BaseAddress;

        public override bool ShowGuideLine { get; }

        public string GuideLineUrl { get; }
    }
}