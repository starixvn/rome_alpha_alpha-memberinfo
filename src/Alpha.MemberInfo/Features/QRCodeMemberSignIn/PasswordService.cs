namespace Alpha.MemberInfo.Features.QRCodeMemberSignIn
{
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Models;
    using Sunplus.Agent.Utilities;
    using System;
    using System.Text.RegularExpressions;

    public interface PasswordService
    {
        PasswordModel CreateMemberPasswordRandom();
    }

    public class PasswordServiceImpl : PasswordService
    {
        private readonly Encryptor encryptor;
        private static readonly Regex MemberPasswordRule = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,10}$"); private static readonly Regex NoWhiteSpace = new Regex(@"^\S*$");

        public PasswordServiceImpl(Encryptor encryptor)
        {
            this.encryptor = encryptor;
        }

        public PasswordModel CreateMemberPasswordRandom()
        {
            var rawPassword = CreateRandomPassword();
            return new PasswordModel(rawPassword, encryptor.CfsEnCode(rawPassword));
        }

        private bool IsMatchedRules(string password)
            => MemberPasswordRule.Match(password).Success && 
               NoWhiteSpace.Match(password).Success;

        private string CreateRandomPassword(int length = 8)
        {
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();

            var isValidPass = false;

            char[] chars = new char[length];

            while (!isValidPass)
            {
                for (int i = 0; i < length; i++)
                {
                    chars[i] = validChars[random.Next(0, validChars.Length)];
                }

                var password = new string(chars); 

                if (!IsMatchedRules(password) ||
               string.Compare(password, "59D7!4$h", StringComparison.OrdinalIgnoreCase) == 0
               || string.Compare(password, "493abcDE", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    isValidPass = false;
                    Array.Clear(chars, 0, length);
                }
                else
                {
                    isValidPass = true;
                }
            }

            return new string(chars);
        }
    }
}
