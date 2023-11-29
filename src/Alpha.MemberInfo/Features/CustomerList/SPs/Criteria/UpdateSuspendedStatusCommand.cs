using Alpha.MemberInfo.Site.Features.CustomerList.Params;

namespace Alpha.MemberInfo.Features.CustomerList.Criteria
{
    public class UpdateSuspendedStatusCommand : UpdateCustomerStatusCommand
    {
        public UpdateSuspendedStatusCommand(UpdateCustomerStatusParam param) : base( param.SearchAccountId, param.ActorId, param.SubAccountId, param.Language, param.IP, param.StatusName)
        {
            Suspended = param.StatusValue;
        }

        public bool Suspended { get; }

    }
}