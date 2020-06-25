namespace Alpha.MemberInfo.Site.Requests.IndirectLoginAccount
{
    using Alpha.MemberInfo.Configurations;
    using Alpha.MemberInfo.Models;
    using MediatR;

    public class ValidateAccountQuery : MemberInfoBaseQuery, IAsyncRequest<Result>
    {
        public ValidateAccountQuery(IUser viewer) : base(viewer)
        {
        }

        public override bool IsValid()
        {
            return base.IsValid() && !IsInternal && IsAllowedIndirectLogin;
        }
    }
}