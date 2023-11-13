namespace Alpha.MemberInfo.Features.CustomerList.Models
{
    using System.Collections.Generic;

    public class CustomerStatusModel
    {
        public CustomerStatusModel(NonProductStatusModel nonProduct, IEnumerable<DCSProductStatusModel> dcsProducts)
        {
            NonProduct = nonProduct;
            DCSProducts = dcsProducts;
        }

        public NonProductStatusModel NonProduct { get; }

        public IEnumerable<DCSProductStatusModel> DCSProducts { get; }
    }
}