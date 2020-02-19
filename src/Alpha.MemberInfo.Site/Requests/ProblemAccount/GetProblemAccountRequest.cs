
namespace Alpha.MemberInfo.Site.Requests.ProblemAccount
{
    using System.Collections.Generic;
    using Alpha.MemberInfo.Enumerations;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Models.ProblemAccount;
    using MediatR;

    public class GetProblemAccountRequest : ProblemAccountRequestBase, IRequest<IEnumerable<ProblemAccountModel>>
    {
        public GetProblemAccountRequest(IUser viewer) : base(viewer)
        {
        }

        public UserLevel SearchRole => Enumeration.FromValue<UserLevel>(SearchRoleId);
    }
}