using Alpha.MemberInfo.Models;
using Alpha.MemberInfo.Site.Features.CopyAccount.Request;

namespace Alpha.MemberInfo.Site.Features.CopyAccount.ViewModels
{
    public class CopyAccountViewModel
    {
        public CopyAccountViewModel(CopyAccountQuery request, string sourceUsername, string usernameSuffix)
        {
            ActorLevel = request.ActorLevel;
            ActorUsername = request.ViewerUsername;
            SourceUsername = sourceUsername;
            UsernameSuffix = usernameSuffix;
            FirstChar = UsernameSuffix.Substring(0, 1).ToUpper();
            SecondChar = UsernameSuffix.Substring(1, 1).ToUpper();
            ThirdChar = request.ActorLevel.IsAgentLevel() ? UsernameSuffix.Substring(2, 1).ToUpper() : string.Empty;
        }

        public CopyAccountViewModel(bool isLimitNewDownline, string errorMessage)
        {
            IsLimitNewDownline = isLimitNewDownline;
            ErrorMessage = errorMessage;
        }

        public UserLevel ActorLevel { get; }

        public string ActorUsername { get; }

        public string SourceUsername { get; }

        public string UsernameSuffix { get; }

        public string[] NameSuffix => new[] {
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
            "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T",
            "U", "V", "W", "X", "Y", "Z" };

        public string ThirdChar { get; }

        public string SecondChar { get; }

        public string FirstChar { get; }

        public string ErrorMessage { get; }

        public bool IsLimitNewDownline { get; }
    }
}