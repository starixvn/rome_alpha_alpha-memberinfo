namespace Alpha.MemberInfo.Site.Features.CustomerList.ViewModels
{
    using Alpha.MemberInfo.Features.CustomerList.Models;
    using Alpha.MemberInfo.Site.Features.CustomerList.Requests;
    using Alpha.Utilities.IP;
    using System.Collections.Generic;
    using System.Linq;

    public class CustomerListPagingViewModel
    {
        public CustomerListPagingViewModel(CustomerListDTO customerListDTO, FetchDownlinesListRequest message, IPService ipService)
        {
            DownlinesList = customerListDTO.DownlinesList.Select(
                 downline =>
                 {
                     if (message.LimitedDateToShowInfo)
                     {
                         downline.CreateDate = "--";
                     }

                     if (!message.ShowNickname)
                     {
                         downline.UserName2 = "--";
                     }

                     if (!message.IsShowMyanmarOdds)
                     {
                         if (message.CustLevel.IsAgentLevel())
                         {
                             var memberList = downline as MemberListModel;
                             memberList.PlayerDiscountMyanmarOdds = 0;
                             memberList.AdiscountMyanmarOdds = 0;
                         }
                         else if (message.CustLevel.IsMasterLevel())
                         {
                             (downline as AgentListModel).DiscountMyanmarOdds = 0;
                         }
                         else
                         {
                             (downline as MasterListModel).DiscountMyanmarOdds = 0;
                         }
                     }

                     var formatedIP = ipService.FormatIP(downline.LastLoginIP, message.IsAdminCanViewVNIP);

                     downline.LastLoginIP = formatedIP.IP;
                     downline.AllowOpenInfoPopup = formatedIP.AllowOpenInfoPopup;

                     return downline;
                 });

            TotalDownlines = customerListDTO.TotalDownlines;
            TotalPages = customerListDTO.TotalPages;
        }

        public IEnumerable<CustomerListModelBase> DownlinesList { get; }

        public int TotalDownlines { get; }

        public int TotalPages { get; }
    }
}