namespace Alpha.MemberInfo.Site.Requests.ProblemAccount
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.ViewModels.ProblemAccount;
    using MediatR;

    public class UpdateProblemAccountRequest : MemberInfoBaseQuery, IRequest<UpdateReviewStatusViewModel>
    {
        public UpdateProblemAccountRequest(IUser viewer) : base(viewer)
        {
        }

        protected readonly byte MemberRoleId = UserLevel.Member.Value;

        public string UserNames { get; set; }

        public string Ids { get; set; }

        public byte RoleId => MemberRoleId;
    }
}