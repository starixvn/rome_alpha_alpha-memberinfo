namespace Alpha.MemberInfo.Site.Features.CustomerList.ViewModels
{
    using Alpha.MemberInfo.Features.CustomerList.Models;
    using Alpha.MemberInfo.Site.Features.CustomerList.Params;
    using System.Collections.Generic;
    using System.Linq;

    public class CustomerListDTO
    {
        public CustomerListDTO(
            IEnumerable<CustomerListModelBase> downlinesList,
            int totalDownlines,
            int totalPages,
            CustomerListParam param)
        {
            DownlinesList = downlinesList.Select(
                downline =>
                {
                    if (!param.LimitedDateToShowInfo)
                    {
                        downline.CreateDate = "--";
                    }

                    if (!param.ShowNickname)
                    {
                        downline.UserName2 = "--";
                    }

                    if (!param.IsShowMyanmarOdds)
                    {
                        if (param.DirectUplineCustLevel.IsAgentLevel())
                        {
                            (downline as MemberListModel).PlayerDiscountMyanmarOdds = 0;
                            (downline as MemberListModel).AdiscountMyanmarOdds = 0;
                        }
                        else if (param.DirectUplineCustLevel.IsMasterLevel())
                        {
                            (downline as AgentListModel).DiscountMyanmarOdds = 0;
                        }
                        else
                        {
                            (downline as MasterListModel).DiscountMyanmarOdds = 0;
                        }
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