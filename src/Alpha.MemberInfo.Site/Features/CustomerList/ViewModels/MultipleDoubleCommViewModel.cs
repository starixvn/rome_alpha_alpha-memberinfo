using Alpha.MemberInfo.Site.Features.CustomerList.Requests;

namespace Alpha.MemberInfo.Site.Features.CustomerList.ViewModels
{
    public class MultipleDoubleCommViewModel
    {
        public MultipleDoubleCommViewModel(MultipleDoubleCommRequest request)
        {
            SearchAccountRoleId = request.SearchAccountRoleId;
            SearchAccountIds = request.SearchAccountIds;
            AgentId = request.AgentId;
        }

        public byte SearchAccountRoleId { get; }

        public string SearchAccountIds { get; }

        public int AgentId { get; }
    }
}