namespace Alpha.MemberInfo.Site.Requests.MemberRequest
{
    using MediatR;
    using MemberInfo.Models;
    using ViewModels.MemberRequest;

    public class ProductQuery :
        MemberInfoBaseQuery,
        IRequest<ProductsViewModel>
    {
        public ProductQuery()
        {
        }

        public ProductQuery(IUser viewer) : base(viewer)
        {
        }

        public int UserSelectedProductId { get; set; }

        public override bool IsValid() => base.IsValid() && CustLevel.IsAgentLevel();
    }
}