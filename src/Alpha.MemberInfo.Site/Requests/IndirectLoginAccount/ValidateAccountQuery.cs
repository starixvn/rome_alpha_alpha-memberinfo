namespace Alpha.MemberInfo.Site.Requests.IndirectLoginAccount
{
    using Alpha.MemberInfo.Models;
    using MediatR;

    public class ValidateAccountQuery : MemberInfoBaseQuery, IAsyncRequest<Result>
    {
        public ValidateAccountQuery(IUser viewer) : base(viewer)
        {
        }
    }
}