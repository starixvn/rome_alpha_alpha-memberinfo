using Alpha.MemberInfo.Site.Features.CustomerList.Params;

namespace Alpha.MemberInfo.Features.CustomerList.Criteria
{
    public class UpdateClosedStatusCommand : UpdateCustomerStatusCommand
    {
        public UpdateClosedStatusCommand(UpdateCustomerStatusParam param) : base( param.SearchAccountId, param.ActorId, param.SubAccountId, param.Language, param.IP, param.StatusName)
        {           
            Closed = param.StatusValue;
            SystemID = 2;
        }

        public bool Closed { get; }

        public int SystemID { get; }

    }
}