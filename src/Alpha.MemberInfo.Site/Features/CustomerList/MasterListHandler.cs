namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn
{
    using Alpha.MemberInfo.Features.CustomerList.Services;
    using Alpha.MemberInfo.Site.Features.CustomerList.Requests;
    using Alpha.MemberInfo.Site.Features.CustomerList.ViewModels;
    using MediatR;
    using System;
    using System.Collections.Generic;

    public class MasterListHandler :
        IRequestHandler<MasterListRequest, IEnumerable<MasterListViewModel>>
    {
        private readonly CustomerListDataService customerListDataService;

        public MasterListHandler(CustomerListDataService customerListDataService)
        {
            this.customerListDataService = customerListDataService;
        }

        public IEnumerable<MasterListViewModel> Handle(MasterListRequest message)
        {
            var viewmodels = new List<MasterListViewModel>();

            for(var i = 1; i < 50; i++)
            {
                viewmodels.Add(new MasterListViewModel
                {
                    No = i,
                    Username = $"Username {i}",
                    Status = "Open",
                    DoubleComm = true,
                    FirstName = "First Name",
                    LastName = "Last Name",
                    LoginIP = "192.168.1.1",
                    CreatedDate = DateTime.Now
                });
            }

            return viewmodels;
        }
    }
}