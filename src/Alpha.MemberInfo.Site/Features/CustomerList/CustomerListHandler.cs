namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn
{
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn;
    using Alpha.MemberInfo.Features.Shared.Models;
    using Alpha.MemberInfo.Site.Features.CustomerList.Requests;
    using Alpha.MemberInfo.Site.Features.CustomerList.ViewModels;
    using MediatR;
    using System.Collections.Generic;
    using System.Linq;

    public class MasterListHandler :
        IRequestHandler<CustomerListRequest, CustomerListViewModel>,
        IRequestHandler<FetchDownlinesListRequest, CustomerListDTO>
    {
        private readonly CustomerListService customerListService;

        public MasterListHandler(CustomerListService customerListService)
        {
            this.customerListService = customerListService;
        }

        public CustomerListViewModel Handle(CustomerListRequest message)
        {
            var breadcrumbLevel = new List<CustInfo>();

            if (message.BreadcrumbLevel != null
                && message.BreadcrumbLevel.Any())
            {
                foreach (var custInfo in message.BreadcrumbLevel)
                {
                    if (custInfo.CustLevelId == message.CustLevelId)
                    {
                        break;
                    }

                    breadcrumbLevel.Add(custInfo);
                }
            }

            breadcrumbLevel.Add(new CustInfo{
               CustId = message.CustId,
               CustName = message.CustName,
               CustLevelId = message.CustLevelId });

            return new CustomerListViewModel(message, breadcrumbLevel);
        }

        public CustomerListDTO Handle(FetchDownlinesListRequest message)
        {
            if (message.CustLevelId == 4)
            {
                return customerListService.GetMasterList(
                    message.CustId,
                    message.Username,
                    message.Enabled,
                    message.Closed,
                    message.Suspended,
                    message.DoubleComm,
                    message.UnconfirmSetting,
                    message.PageSize,
                    message.PageIndex);
            }
            else if (message.CustLevelId == 3)
            {
                return customerListService.GetAgentList(
               message.ViewerId,
               message.CustId,
              message.Username,
               message.Enabled,
                    message.Closed,
                    message.Suspended,
                message.DoubleComm,
                    message.UnconfirmSetting,
                message.PageSize,
                    message.PageIndex);
            }
            else
            {
                return customerListService.GetMemberList(
             message.ViewerId,
               message.CustId,
             message.Username,
              message.Enabled,
                    message.Closed,
                    message.Suspended,
               message.DoubleComm,
                    message.UnconfirmSetting,
               message.PageSize,
                    message.PageIndex);
            }
        }
    }
}