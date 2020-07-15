namespace Alpha.MemberInfo.Site.Features.CustomerList.ViewModels
{
    using Alpha.MemberInfo.Features.CustomerList.Models;
    using System.Collections.Generic;

    public class MasterListViewModel
    {
        public MasterListViewModel(
            IEnumerable<MasterListModel> downlinesList,
            int totalDownlines,
            int totalPages)
        {
            DownlinesList = downlinesList;
            TotalDownlines = totalDownlines;
            TotalPages = totalPages;
        }

        public IEnumerable<MasterListModel> DownlinesList { get; set; }

        public int TotalDownlines { get; set; }

        public int TotalPages { get; set; }
    }
}