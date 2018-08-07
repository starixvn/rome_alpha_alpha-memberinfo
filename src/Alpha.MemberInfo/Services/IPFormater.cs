namespace Alpha.MemberInfo.Services
{
    using Sunplus.Core.Common.IPs;

    public sealed class IPFormater : IPFormat
    {
        public IPFormater(string ip, int adminId, bool isAdminCanViewVNIP)
            : base(ip)
        {
            Context = new ContextIPInfo
            {
                AdminId = adminId,
                IsAdminCanViewVNIP = isAdminCanViewVNIP
            };
        }

        protected override IContextIPInfo Context { get; }

        protected override string LinkFormat { get; }
            = "<a href=\"javascript:;\" class=\"link iplink column-loginip\">{0}</a>";

        protected override string LinkNoFormat { get; } = "{0}";
    }
}
