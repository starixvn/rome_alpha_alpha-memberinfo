namespace Alpha.MemberInfo.Site.Requests.IndirectLoginAccount
{
    using Alpha.MemberInfo.Models;

    public class UpdatePasswordCommand : NewAccountCommand
    {
        public UpdatePasswordCommand(IUser viewer) : base(viewer)
        {
        }
    }
}