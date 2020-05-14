namespace Alpha.MemberInfo.Site.Requests.IndirectLoginAccount
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.ViewModels.IndirectLoginAccount;
    using MediatR;

    public class ManageAccountQuery : MemberInfoBaseQuery, IAsyncRequest<ManageAccountViewModel>
    {
        public ManageAccountQuery(IUser viewer) : base(viewer)
        {
        }
    }
}