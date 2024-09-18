namespace Alpha.MemberInfo.Features.MemberBettingBehavior.Models
{
    using System.Collections.Generic;

    public class CTSCustomerClassCategoryResult
    {
        public int StatusCode { get; set; }

        public string ErrorMessage { get; set; }

        public IEnumerable<CustomerClassCategory> Detail { get; set; }
    }
}
