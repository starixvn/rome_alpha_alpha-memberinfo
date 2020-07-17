namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn
{
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn;
    using Alpha.MemberInfo.Site.Features.CustomerList.Requests;
    using Alpha.MemberInfo.Site.Features.CustomerList.ViewModels;
    using MediatR;

    public class MasterListHandler :
        IRequestHandler<MasterListRequest, MasterListViewModel>
    {
        private readonly CustomerListService customerListService;

        public MasterListHandler(CustomerListService customerListService)
        {
            this.customerListService = customerListService;
        }

        public MasterListViewModel Handle(MasterListRequest message)
        {
            if (message.SearchAccountRoleId == 4)
            {
                return new MasterListViewModel(
                    customerListService.GetMasterList(
                    1176070,
                    string.Empty,
                    null,
                    null,
                    null,
                    2,
                    0,
                    500,
                    1),
                    500,
                    1,
                    message);
            }
            else
            {
                return new MasterListViewModel(
               customerListService.GetAgentList(
               1176070,
               1176083,
               string.Empty,
               null,
               null,
               null,
               2,
               0,
               500,
               1),
               500,
               1,
               message);
            }
        }
    }
}