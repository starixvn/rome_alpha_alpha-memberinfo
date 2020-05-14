namespace Alpha.MemberInfo.Site.Requests.IndirectLoginAccount
{
    using Alpha.MemberInfo.Models;
    using MediatR;

    public class DisconnectCommand : MemberInfoBaseQuery, IAsyncRequest<Result>
    {
        public DisconnectCommand(IUser viewer) : base(viewer)
        {
        }

        public override bool IsValid()
            => base.IsValid() && !IsInternal;
    }
}