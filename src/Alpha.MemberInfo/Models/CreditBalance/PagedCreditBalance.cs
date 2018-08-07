namespace Alpha.MemberInfo.Models.CreditBalance
{
    using System.Collections.Generic;

    public class PagedCreditBalance
    {
        public PagedCreditBalance(
            int pageIndex,
            int pageSize,
            int totalItems,
            IEnumerable<CreditBalanceItem> creditBalances)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalItems = totalItems;
            CreditBalances = creditBalances;
            ShowPager = TotalItems > 0;
        }

        public int PageIndex { get; }

        public int PageSize { get; }

        public int TotalItems { get; }

        public IEnumerable<CreditBalanceItem> CreditBalances { get; }

        public bool ShowPager { get; }
    }
}
