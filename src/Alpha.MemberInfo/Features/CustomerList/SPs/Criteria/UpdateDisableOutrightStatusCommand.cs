using Alpha.MemberInfo.Site.Features.CustomerList.Params;

namespace Alpha.MemberInfo.Features.CustomerList.Criteria
{
    public class UpdateDisableOutrightStatusCommand : UpdateCustomerStatusCommand
    {
        public UpdateDisableOutrightStatusCommand(UpdateCustomerStatusParam param) : base( param.SearchAccountId, param.ActorId, param.SubAccountId, param.Language, param.IP, param.StatusName)
        {
            DisableOutright = param.StatusValue;
        }

        public bool DisableOutright { get; }

    }
}