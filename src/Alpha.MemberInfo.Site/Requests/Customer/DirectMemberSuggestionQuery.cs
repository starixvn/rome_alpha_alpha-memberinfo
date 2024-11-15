using Alpha.MemberInfo.Models;

namespace Alpha.MemberInfo.Site.Requests.Customer
{
    public class DirectMemberSuggestionQuery : MemberInfoBaseQuery
    {
        public DirectMemberSuggestionQuery(IUser viewer)
            : base(viewer)
        {
        }

        public string SearchUsername { get; set; }
    }
}