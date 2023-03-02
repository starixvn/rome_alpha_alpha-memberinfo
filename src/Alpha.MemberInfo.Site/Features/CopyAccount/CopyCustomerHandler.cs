using Alpha.MemberInfo.Features.QRCodeMemberSignIn;
using Alpha.MemberInfo.Site.Features.CopyAccount.Request;
using Alpha.MemberInfo.Site.Features.CopyAccount.ViewModels;
using MediatR;
using System.Threading.Tasks;

namespace Alpha.MemberInfo.Site.Features.CopyAccount
{
    public class CopyCustomerHandler : IAsyncRequestHandler<CopyAccountQuery, CopyAccountViewModel>
    {
        private readonly QRCodeMemberSignInService qrCodeMemberSignInService;

        public CopyCustomerHandler(QRCodeMemberSignInService qrCodeMemberSignInService)
        {
            this.qrCodeMemberSignInService = qrCodeMemberSignInService;
        }

        public Task<CopyAccountViewModel> Handle(CopyAccountQuery message)
        {
            var limitNewDownlineResult = qrCodeMemberSignInService.CheckLimitNewDownline(
                   message.ViewerId,
                   message.ViewerLevelId,
                   message.Language);

            if (limitNewDownlineResult.IsFailed)
            {
                return Task.FromResult(new CopyAccountViewModel(true, limitNewDownlineResult.Message));
            }


            return Task.FromResult(new CopyAccountViewModel(
                message,
                message.SourceUsername, 
                qrCodeMemberSignInService.GetSuggestUsername(message.CustName, message.CustId)));
        }
    }
}