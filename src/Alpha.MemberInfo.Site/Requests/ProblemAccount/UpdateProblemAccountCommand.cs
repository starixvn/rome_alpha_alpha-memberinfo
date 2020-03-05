namespace Alpha.MemberInfo.Site.Requests.ProblemAccount
{
    using Alpha.MemberInfo.Models;
    using MediatR;

    public class UpdateProblemAccountCommand : MemberInfoBaseQuery, IRequest<Result>
    {
        private readonly IUser viewer;

        public UpdateProblemAccountCommand(IUser viewer):base(viewer)
        {
            this.viewer = viewer;
        }

        public bool IsAffectedAllDownlines { get; set; }

        public string Ids { get; set; }

        public byte RoleId { get; set; }

        public override bool IsValid()
        {
            return base.IsValid() && !viewer.IsInternal;
        }
    }
}