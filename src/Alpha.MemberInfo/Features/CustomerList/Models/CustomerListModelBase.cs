namespace Alpha.MemberInfo.Features.CustomerList.Models
{
    public class CustomerListModelBase
    {
        public int CustId { get; set; }

        public string Username { get; set; }

        public byte RoleId { get; set; }

        public string UGroup { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName2 { get; set; }

        public string Phone { get; set; }

        public string MobilePhone { get; set; }

        public string LastLoginIP { get; set; }

        public int Currency { get; set; }

        public int MatchLimit { get; set; }

        public int VirtualMultiplier { get; }

        public string CreateDate { get; set; }

        public bool IsInternal { get; set; }

        public bool DoubleCommLosing { get; set; }

        public int ClosedReason { get; set; }

        public int RowNumber { get; set; }

        public bool Enabled { get; set; }

        public bool Closed { get; set; }

        public bool Suspended { get; set; }

        public bool DisableOutright { get; set; }

        public decimal DiscountCS { get; set; }

        public decimal Discount1x2 { get; set; }

        public string Status
        {
            get
            {
                if(Enabled)
                {
                    if (Closed)
                    {
                        return "Closed";
                    }
                    else if (Suspended)
                    {
                        return "Suspended";
                    }
                    else
                    {
                        return "Open";
                    }
                }
                else
                {
                    return "Disable";
                }
            }
        }
    }
}
