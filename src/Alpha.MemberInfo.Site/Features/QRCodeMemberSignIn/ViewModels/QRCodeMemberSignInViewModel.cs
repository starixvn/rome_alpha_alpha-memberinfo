namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.ViewModels
{
    public class QRCodeMemberSignInViewModel
    {
        public QRCodeMemberSignInViewModel(string memberName)
        {
            MemberName = memberName;
        }

        public string MemberName { get; }
    }
}