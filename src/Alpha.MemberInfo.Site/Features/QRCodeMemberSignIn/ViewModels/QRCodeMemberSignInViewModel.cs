namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.ViewModels
{
    public class QRCodeMemberSignInViewModel
    {
        public QRCodeMemberSignInViewModel(
            string memberNameCopyFrom, 
            string actorName,
            decimal credit, 
            string memberNameSuffix,
            string errorMessage)
        {
            MemberNameCopyFrom = memberNameCopyFrom;
            ActorName = actorName;
            ErrorMessage = errorMessage;
            Credit = credit;
            MemberNameSuffix = memberNameSuffix;
        }

        public string MemberNameCopyFrom { get; }

        public string ActorName { get; }

        public decimal Credit { get; }

        public string[] NameSuffix => new[] {
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
            "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T",
            "U", "V", "W", "X", "Y", "Z" };

        public string MemberNameSuffix { get; }

        public string ThirdChar => MemberNameSuffix.Substring(2, 1).ToUpper();

        public string SecondChar => MemberNameSuffix.Substring(1, 1).ToUpper();

        public string FirstChar => MemberNameSuffix.Substring(0, 1).ToUpper();

        public string ErrorMessage { get; }
    }
}