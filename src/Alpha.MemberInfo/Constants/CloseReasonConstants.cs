namespace Alpha.MemberInfo.Constants
{
    using System.Collections.Generic;

    public static class CloseReasonConstants
    {
        public const int WrongPassword = 0;
        public const int ByAdmin = 1;
        public const int ByUpline = 2;
        public const int WrongOTPSC = 3;
        public const int ByArchive = 4;

        public static IDictionary<int, string> Items { get; } = new Dictionary<int, string>
        {
            [WrongPassword] = nameof(WrongPassword),
            [ByAdmin] = nameof(ByAdmin),
            [ByUpline] = nameof(ByUpline),
            [WrongOTPSC] = nameof(WrongOTPSC),
            [ByArchive] = nameof(ByArchive)
        };

        public static string GetName(int key)
            => Items.ContainsKey(key) ? Items[key] : string.Empty;
    }
}
