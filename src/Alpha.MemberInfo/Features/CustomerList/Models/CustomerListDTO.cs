namespace Alpha.MemberInfo.Site.Features.CustomerList.ViewModels
{
    using Alpha.MemberInfo.Features.CustomerList.Models;
    using System.Collections.Generic;

    public class CustomerListDTO
    {
        public CustomerListDTO(
            IEnumerable<CustomerListModelBase> downlinesList,
             int pageSize,
            int pageIndex,
            int totalDownlines,
            int totalPages)
        {
            DownlinesList = downlinesList;
            TotalDownlines = totalDownlines;
            TotalPages = totalPages;
            PageSize = pageSize;
            PageIndex = pageIndex;
        }

        public IEnumerable<CustomerListModelBase> DownlinesList { get; }

        public int PageSize { get; }

        public int PageIndex { get; }

        public int TotalDownlines { get; }

        public int TotalPages { get; }

    }
}