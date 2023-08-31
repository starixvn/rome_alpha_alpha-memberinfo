using Alpha.MemberInfo.Site.Features.CustomerList.Params;

namespace Alpha.MemberInfo.Features.CustomerList.Criteria
{
    public class UpdateUnlockedCommand : UpdateCustomerStatusCommand
    {
        public UpdateUnlockedCommand(UpdateCustomerStatusParam param) : base( param.SearchAccountId, param.ActorId, param.SubAccountId, param.Language, param.IP, param.StatusName)
        {
        }
    }
}