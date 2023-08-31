using Alpha.MemberInfo.Site.Features.CustomerList.Params;

namespace Alpha.MemberInfo.Features.CustomerList.Criteria
{
    public class UpdateDoubleCommLosingStatusCommand : UpdateCustomerStatusCommand
    {
        public UpdateDoubleCommLosingStatusCommand(UpdateCustomerStatusParam param) : base( param.SearchAccountId, param.ActorId, param.SubAccountId, param.Language, param.IP, param.StatusName)
        {
            DoubleCommLosing = param.StatusValue;
        }

        public bool DoubleCommLosing { get; }

    }
}