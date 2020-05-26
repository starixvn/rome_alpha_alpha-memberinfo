namespace Alpha.MemberInfo.Features.QRCodeMemberSignIn.Models
{
    public class UsernameSuggestion
    {
        public UsernameSuggestion()
        {
        }

        public UsernameSuggestion(string userName, string firstName, string lastName, string username2)
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            UserName2 = username2;
        }

        public string UserName { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string UserName2 { get; private set; }
    }
}
