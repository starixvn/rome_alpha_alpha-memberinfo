namespace Alpha.MemberInfo.Site.Features.CustomerList.ViewModels
{
    using Alpha.MemberInfo.Features.CustomerList.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class CustomerListDTO
    {
        public CustomerListDTO(
            IEnumerable<CustomerListModelBase> downlinesList,
            int totalDownlines,
            int totalPages,
            bool limitedDateToShowInfo,
            bool showNickname)
        {
            DownlinesList = downlinesList.Select(
                downline =>
                {
                    if (!limitedDateToShowInfo)
                    {
                        downline.CreateDate = "--";
                    }

                    if (!showNickname)
                    {
                        downline.UserName2 = "--";
                    }

                    return downline;
                });

            TotalDownlines = totalDownlines;
            TotalPages = totalPages;
        }

        public IEnumerable<CustomerListModelBase> DownlinesList { get; }

        public int TotalDownlines { get; }

        public int TotalPages { get; }
    }
}