namespace Alpha.MemberInfo.Features.CustomerList.Models
{
    public class MemberListModel : CustomerListModelBase
    {
        public MemberListModel()
        {
        }

        public MemberListModel(
         int custId,
         string username,
         byte roleId,
         string uGroup,
         string firstName,
         string lastName,
         string userName2,
         string phone,
         string mobilePhone,
         string lastLoginIP,
         int currency,
         int matchLimit,
         int virtualMultiplier,
         string createDate,
         bool isInternal,
         bool doubleCommLosing,
         int closedReason,
         int rowNumber,
         bool enabled,
         bool closed,
         bool suspended,
         bool disableOutright,
         decimal discountCS,
         decimal discount1x2,
         decimal playerDiscount,
         decimal playerDiscountCS,
         decimal playerDiscount1x2,
         decimal discount)
            : base(custId,
            username,
            roleId,
            uGroup,
            firstName,
            lastName,
            userName2,
            phone,
            mobilePhone,
            lastLoginIP,
            currency,
            matchLimit,
            virtualMultiplier,
            createDate,
            isInternal,
            doubleCommLosing,
            closedReason,
            rowNumber,
            enabled,
            closed,
            suspended,
            disableOutright,
            discountCS,
            discount1x2)
        {
            PlayerDiscount = playerDiscount;
            PlayerDiscountCS = playerDiscountCS;
            PlayerDiscount1x2 = playerDiscount1x2;
            Discount = discount;
        }

        public decimal PlayerDiscount { get; private set; }

        public decimal PlayerDiscountCS { get; private set; }

        public decimal PlayerDiscount1x2 { get; private set; }

        public decimal Discount { get; private set; }
    }
}
