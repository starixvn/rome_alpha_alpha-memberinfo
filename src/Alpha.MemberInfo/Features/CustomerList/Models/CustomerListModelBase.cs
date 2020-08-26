using Alpha.MemberInfo.Features.CustomerList.AppResources.CustomerListResources;

namespace Alpha.MemberInfo.Features.CustomerList.Models
{
    public class CustomerListModelBase
    {
        public CustomerListModelBase()
        { }

        public CustomerListModelBase(
         int custId,
         string username,
         byte roleId,
         string uGroup,
         string firstName,
         string lastName ,
         string userName2,
         string phone,
         string mobilePhone,
         string lastLoginIP,
         int currency,
         int matchLimit ,
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
         decimal discount1x2)
        {
            CustId = custId;
            Username = username;
            RoleId = roleId;
            UGroup = uGroup;
            FirstName = firstName;
            LastName = lastName;
            UserName2 = userName2;
            Phone = phone;
            MobilePhone = mobilePhone;
            LastLoginIP = lastLoginIP;
            Currency = currency;
            MatchLimit = matchLimit;
            VirtualMultiplier = virtualMultiplier;
            CreateDate = createDate;
            DoubleCommLosing = doubleCommLosing;
            ClosedReason = closedReason;
            RowNumber = rowNumber;
            Enabled = enabled;
            Closed = closed;
            Suspended = suspended;
            DisableOutright = disableOutright;
            DiscountCS = discountCS;
            Discount1x2 = discount1x2;
        }

        public int CustId { get; private set; }

        public string Username { get; private set; }

        public byte RoleId { get; private set; }

        public string UGroup { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string UserName2 { get; private set; }

        public string Phone { get; private set; }

        public string MobilePhone { get; private set; }

        public string LastLoginIP { get; private set; }

        public int Currency { get; private set; }

        public int MatchLimit { get; private set; }

        public int VirtualMultiplier { get; private set; }

        public string CreateDate { get; set; }

        public bool IsInternal { get; private set; }

        public bool DoubleCommLosing { get; private set; }

        public int ClosedReason { get; private set; }

        public int RowNumber { get; private set; }

        public bool Enabled { get; private set; }

        public bool Closed { get; private set; }

        public bool Suspended { get; private set; }

        public bool DisableOutright { get; private set; }

        public decimal DiscountCS { get; private set; }

        public decimal Discount1x2 { get; private set; }

        public string Status
        {
            get
            {
                if(Enabled)
                {
                    if (Closed)
                    {
                        return CustomerListResources.Closed;
                    }
                    else if (Suspended)
                    {
                        return CustomerListResources.Suspended;
                    }
                    else
                    {
                        return CustomerListResources.Open;
                    }
                }
                else
                {
                    return CustomerListResources.Disabled;
                }
            }
        }
    }
}
