namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Requests;

    public class QRCodeBaseQuery : MemberInfoBaseQuery
    {
        public QRCodeBaseQuery(IUser viewer) : base(viewer)
        {
        }

        public override bool IsValid() => base.IsValid() && ActorLevel.IsAgentLevel();
    }
}