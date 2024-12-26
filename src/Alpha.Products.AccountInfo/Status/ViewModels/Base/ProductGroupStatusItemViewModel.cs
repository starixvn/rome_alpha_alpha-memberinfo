using System;
using Alpha.Products.AccountInfo.Requests;
using Alpha.Products.Core.Shared;

namespace Alpha.Products.AccountInfo.Status.ViewModels.Base
{
    public class ProductGroupStatusItemViewModel : StatusItemViewModel
    {
        private const int DisplayOrderStep = 10;

        public ProductGroupStatusItemViewModel(ISearchAccountInfoRequest request, int productGroupId)
            : base(request)
        {
            ProductGroupId = productGroupId;
        }

        public int ProductGroupId { get; }

        public override int DisplayOrder => ProductGroupId * DisplayOrderStep;

        public override string Text => Request.GetProductGroupName(ProductGroupId);

        public override string Url => AccountInfoUrl + "ProductStatus?" + string.Join("&",
                                           $"isGroup=true",
                                           $"productid={ProductGroupId}",
                                           $"custId={SearchAccount.Id}",
                                           $"custName={SearchAccount.Username}",
                                           $"custRoleId={SearchAccount.CusomerLevelValue}",
                                           $"agentId={SearchAccount.AgentId}",
                                           $"uid={Guid.NewGuid()}");

        public override bool Visible => ProductGroupId != ProductConstants.Sportsbook && Actor.IsDirectUplineOf(SearchAccount);

        public override bool Checked => true;
    }
}