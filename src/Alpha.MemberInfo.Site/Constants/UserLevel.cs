using System.Collections.Generic;

namespace Alpha.MemberInfo.Site.Constants
{
    public static class UserLevel
    {
        public const int Member = 1;
        public const int Agent = 2;
        public const int Master = 3;
        public const int Super = 4;

        private static readonly Dictionary<int, string> levels = new Dictionary<int, string>
        {
            [Member] = "Member",
            [Agent] = "Agent",
            [Master] = "Master",
            [Super] = "Super"
        };

        public static string GetDisplayName(int level) => levels[level];
    }
}