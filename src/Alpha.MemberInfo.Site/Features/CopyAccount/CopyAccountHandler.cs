using Alpha.MemberInfo.AppResources;
using Alpha.MemberInfo.Features.QRCodeMemberSignIn;
using Alpha.MemberInfo.Site.Features.CopyAccount.Request;
using Alpha.MemberInfo.Site.Features.CopyAccount.ViewModels;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Alpha.MemberInfo.Site.Features.CopyAccount
{
    public class CopyAccountHandler : IAsyncRequestHandler<CopyAccountQuery, CopyAccountViewModel>, IAsyncRequestHandler<CopyAccountCommand, CopyAccountResponse>
    {
        private readonly QRCodeMemberSignInService qrCodeMemberSignInService;
        private readonly ICopyAccountService copyAccountService;

        public CopyAccountHandler(QRCodeMemberSignInService qrCodeMemberSignInService, ICopyAccountService copyAccountService)
        {
            this.qrCodeMemberSignInService = qrCodeMemberSignInService;
            this.copyAccountService = copyAccountService;
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
                qrCodeMemberSignInService.GetSuggestUsername(message.ViewerUsername, message.ViewerId, message.ViewerLevelId)));
        }

        public async Task<CopyAccountResponse> Handle(CopyAccountCommand message)
        {
            var limitNewDownlineResult = qrCodeMemberSignInService.CheckLimitNewDownline(
                message.ViewerId,
                message.ViewerLevelId,
                message.Language);

            if (limitNewDownlineResult.IsFailed)
            {
                return new CopyAccountResponse
                {
                    Success = false,
                    ErrCode = limitNewDownlineResult.Code,
                    ErrMsg = limitNewDownlineResult.Message
                };
            }

            var creditAndStatusModel = qrCodeMemberSignInService.GetCreditAndStatus(message.SourceUsername);

            if (creditAndStatusModel.Status != null &&
              !creditAndStatusModel.Status.Equals("OPEN", StringComparison.OrdinalIgnoreCase))
            {
                return new CopyAccountResponse
                {
                    Success = false,
                    ErrCode = 100,
                    ErrMsg = Resources.CannotCreateClosedMemberQRCode
                };
            }

            var model = qrCodeMemberSignInService.CheckBelongingByUsername(message.CustId, message.SourceUsername, message.SubAccId);
            var sourceCustId = model.CustId;

            if (sourceCustId > 0 && qrCodeMemberSignInService.IsEnableCustomer(sourceCustId))
            {
                return await copyAccountService.CreateAccount(message, sourceCustId);
            }

            return new CopyAccountResponse
            {
                Success = false,
                ErrCode = 100,
                ErrMsg = Resources.UsernameIsInvalid
            };
        }
    }
}