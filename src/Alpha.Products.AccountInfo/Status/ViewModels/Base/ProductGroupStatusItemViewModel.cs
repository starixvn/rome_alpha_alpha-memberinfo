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

        public override string Url => string.Empty;

        public override bool Visible => ProductGroupId != ProductConstants.Sportsbook;

        public override bool Checked => true;
    }
}