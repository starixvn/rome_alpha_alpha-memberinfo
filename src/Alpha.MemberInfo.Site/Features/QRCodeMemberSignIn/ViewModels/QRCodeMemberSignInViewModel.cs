namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.ViewModels
{
    public class QRCodeMemberSignInViewModel
    {
        public QRCodeMemberSignInViewModel(string memberName, decimal credit, string errorMessage)
        {
            MemberName = memberName;
            ErrorMessage = errorMessage;
            Credit = credit;
        }

        public string MemberName { get; }

        public decimal Credit { get; }

        public string ErrorMessage { get; }
    }
}