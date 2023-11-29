using Alpha.MemberInfo.Site.Features.CustomerList.Params;

namespace Alpha.MemberInfo.Features.CustomerList.Criteria
{
    public class UpdateAllowAutoTransferStatusCommand : UpdateCustomerStatusCommand
    {
        public UpdateAllowAutoTransferStatusCommand(UpdateCustomerStatusParam param) : base( param.SearchAccountId, param.ActorId, param.SubAccountId, param.Language, param.IP, param.StatusName)
        {
            AllowAutoTransfer = param.StatusValue;
        }

        public bool AllowAutoTransfer { get; }

    }
}