namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn
{
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn;
    using Alpha.MemberInfo.Site.Features.CustomerList.Requests;
    using Alpha.MemberInfo.Site.Features.CustomerList.ViewModels;
    using MediatR;

    public class MasterListHandler :
        IRequestHandler<MasterListRequest, MasterListViewModel>,
        IRequestHandler<FetchDownlinesListRequest, CustomerListDTO>
    {
        private readonly CustomerListService customerListService;

        public MasterListHandler(CustomerListService customerListService)
        {
            this.customerListService = customerListService;
        }

        public MasterListViewModel Handle(MasterListRequest message)
            => new MasterListViewModel(message);

        public CustomerListDTO Handle(FetchDownlinesListRequest message)
        {
            if (message.SearchAccountRoleId == 4)
            {
                return customerListService.GetMasterList(
                    1176070,
                    string.Empty,
                    null,
                    null,
                    null,
                    2,
                    0,
                    message.PageSize,
                    message.PageIndex);
            }
            else if (message.SearchAccountRoleId == 3)
            {
                return customerListService.GetAgentList(
               1176070,
               1176083,
               string.Empty,
               null,
               null,
               null,
               2,
               0,
               500,
               1);
            }
            else
            {
                return customerListService.GetMemberList(
              1176070,
              1176086,
              string.Empty,
              null,
              null,
              null,
              2,
              0,
              500,
              1);
            }
        }
    }
}