namespace Alpha.MemberInfo.Site.Requests.ProblemAccount
{
    using Alpha.MemberInfo.Models;
    using MediatR;

    public class UpdateProblemAccountCommand : MemberInfoBaseQuery, IRequest<Result>
    {
        public UpdateProblemAccountCommand(IUser viewer) : base(viewer)
        {
        }

        protected readonly byte MemberRoleId = UserLevel.Member.Value;

        public bool IsAffectedAllDownlines { get; set; }

        public string Ids { get; set; }

        public byte RoleId => MemberRoleId;

        public override bool IsValid()
            => base.IsValid() && !IsInternal;
    }
}