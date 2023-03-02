using Alpha.MemberInfo.Models;
using Alpha.MemberInfo.Site.Features.CopyAccount.Request;

namespace Alpha.MemberInfo.Site.Features.CopyAccount.ViewModels
{
    public class CopyAccountViewModel
    {
        public CopyAccountViewModel(CopyAccountQuery request, string sourceUsername, string usernameSuffix)
        {
            ActorUsername = request.ViewerUsername;
            SourceUsername = sourceUsername;
            UsernameSuffix = usernameSuffix;
        }

        public CopyAccountViewModel(bool isLimitNewDownline, string errorMessage)
        {
            IsLimitNewDownline = isLimitNewDownline;
            ErrorMessage = errorMessage;
        }

        public string ActorUsername { get; }

        public string SourceUsername { get; }

        public string UsernameSuffix { get; }

        public string[] NameSuffix => new[] {
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
            "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T",
            "U", "V", "W", "X", "Y", "Z" };

        public string ThirdChar => UsernameSuffix.Substring(2, 1).ToUpper();

        public string SecondChar => UsernameSuffix.Substring(1, 1).ToUpper();

        public string FirstChar => UsernameSuffix.Substring(0, 1).ToUpper();

        public string ErrorMessage { get; }

        public bool IsLimitNewDownline { get; }
    }
}