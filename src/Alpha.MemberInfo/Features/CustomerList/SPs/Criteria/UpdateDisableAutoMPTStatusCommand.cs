using Alpha.MemberInfo.Site.Features.CustomerList.Params;

namespace Alpha.MemberInfo.Features.CustomerList.Criteria
{
    public class UpdateDisableAutoMPTStatusCommand : UpdateCustomerStatusCommand
    {
        public UpdateDisableAutoMPTStatusCommand(UpdateCustomerStatusParam param) : base( param.SearchAccountId, param.ActorId, param.SubAccountId, param.Language, param.IP, param.StatusName)
        {
            DisableAutoMPT = param.StatusValue;
        }

        public bool DisableAutoMPT { get; }

    }
}