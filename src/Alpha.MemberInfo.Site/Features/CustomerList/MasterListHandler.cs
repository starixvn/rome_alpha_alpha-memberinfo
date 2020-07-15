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
            //var viewmodels = new List<MasterListViewModel>();

            //for(var i = 1; i < 50; i++)
            //{
            //    viewmodels.Add(new MasterListViewModel
            //    {
            //        No = i,
            //        Username = $"Username {i}",
            //        Status = "Open",
            //        DoubleComm = true,
            //        FirstName = "First Name",
            //        LastName = "Last Name",
            //        LoginIP = "192.168.1.1",
            //        CreatedDate = DateTime.Now
            //    });
            //}

            //return viewmodels;
        }
    }
}