using System.Diagnostics.CodeAnalysis;
using Alpha.MemberInfo.Models;

namespace Alpha.MemberInfo.Tests.ModelsForTesting
{
    public class UserLevelForTesting : UserLevel
    {
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Reviewed!")]
        public static readonly new UserLevelForTesting Agent = new UserLevelForTesting(1, "Super");

        public UserLevelForTesting()
        {
        }

        private UserLevelForTesting(byte value, string displayName)
        {
        }
    }
}
