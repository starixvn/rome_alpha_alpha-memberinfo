namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn
{
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn;
    using Alpha.MemberInfo.Site.Features.CustomerList.ViewModels;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Helpers;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.ViewModels;
    using Alpha.Products.Core.Services;
    using MediatR;
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

            for(var i = 1; i < 100; i++)
            {
                viewmodels.Add(new MasterListViewModel
                {
                    No = i,
                    Username = $"Username {i}",
                    Status = "Open",
                    DoubleComm = true,
                    FirstName = "First Name",
                    LastName = "Last Name",
                    LoginIP = "192.168.1.1"
                });
            }

            return viewmodels;
        }
           
    }
}