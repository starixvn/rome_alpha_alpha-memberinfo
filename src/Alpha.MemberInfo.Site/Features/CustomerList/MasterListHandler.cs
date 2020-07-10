namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn
{
    using Alpha.MemberInfo.Site.Features.CustomerList.ViewModels;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests;
    using MediatR;
    using System;
    using System.Collections.Generic;

    public class MasterListHandler :
        IRequestHandler<MasterListRequest, IEnumerable<MasterListViewModel>>
    {

        public MasterListHandler()
        {
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