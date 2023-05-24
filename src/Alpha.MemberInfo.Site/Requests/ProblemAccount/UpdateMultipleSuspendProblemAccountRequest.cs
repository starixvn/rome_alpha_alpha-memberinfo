namespace Alpha.MemberInfo.Site.Requests.ProblemAccount
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.ViewModels.ProblemAccount;
    using MediatR;

    public class UpdateMultipleSuspendProblemAccountRequest : MemberInfoBaseQuery, IRequest<UpdateMultipleSuspendViewModel>
    {
        public UpdateMultipleSuspendProblemAccountRequest(IUser viewer) : base(viewer)
        {
        }
        public string UserNames { get; set; }

        public string Ids { get; set; }

        public byte RoleId { get; set; }
    }
}