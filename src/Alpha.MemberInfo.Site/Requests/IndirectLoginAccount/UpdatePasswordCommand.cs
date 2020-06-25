namespace Alpha.MemberInfo.Site.Requests.IndirectLoginAccount
{
    using Alpha.MemberInfo.Configurations;
    using Alpha.MemberInfo.Models;

    public class UpdatePasswordCommand : NewAccountCommand
    {
        public UpdatePasswordCommand(IUser viewer) : base(viewer)
        {
        }

        public override bool IsValid()
        {
            return base.IsValid() && !IsInternal && IsAllowedIndirectLogin;
        }
    }
}