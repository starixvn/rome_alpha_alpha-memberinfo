namespace Alpha.MemberInfo.Site.Requests.IndirectLoginAccount
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.ViewModels.IndirectLoginAccount;
    using MediatR;

    public class UpdatePasswordQuery : MemberInfoBaseQuery, IRequest<UpdatePasswordViewModel>
    {
        public UpdatePasswordQuery(IUser viewer) : base(viewer)
        {
        }
    }
}