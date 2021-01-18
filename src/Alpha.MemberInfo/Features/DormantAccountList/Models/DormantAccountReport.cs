namespace Alpha.MemberInfo.Features.DormantAccountList.Models
{
    using System.Collections.Generic;

    public class DormantAccountReport
    {
        public DormantAccountReport(
            IEnumerable<DormantAccount> dormantAccounts,
            DormantAccount total,
            int totalItems)
        {
            DormantAccounts = dormantAccounts;
            Total = total;
            TotalItems = totalItems;
        }

        public IEnumerable<DormantAccount> DormantAccounts { get; }

        public DormantAccount Total { get; }

        public int TotalItems { get; }

        public bool HasData => TotalItems > 0;
    }
}
