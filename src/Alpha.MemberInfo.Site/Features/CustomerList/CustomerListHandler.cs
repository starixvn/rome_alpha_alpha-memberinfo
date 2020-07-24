namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn
{
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn;
    using Alpha.MemberInfo.Site.Features.CustomerList.Requests;
    using Alpha.MemberInfo.Site.Features.CustomerList.ViewModels;
    using MediatR;

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
            => new CustomerListViewModel(message);

        public CustomerListDTO Handle(FetchDownlinesListRequest message)
        {
            if (message.SearchAccountRoleId == 4)
            {
                return customerListService.GetMasterList(
                    message.SearchAccountId,
                    message.Username,
                    message.Enabled,
                    message.Closed,
                    message.Suspended,
                    message.DoubleComm,
                    message.UnconfirmSetting,
                    message.PageSize,
                    message.PageIndex);
            }
            else if (message.SearchAccountRoleId == 3)
            {
                return customerListService.GetAgentList(
               message.ActorId,
               message.SearchAccountId,
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
             message.ActorId,
               message.SearchAccountId,
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