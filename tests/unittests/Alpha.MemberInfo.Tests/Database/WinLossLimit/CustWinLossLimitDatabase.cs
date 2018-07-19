namespace Alpha.MemberInfo.Tests.Database.WinLossLimit
{
    using System.Data;
    using Helpers;

    public static class CustWinLossLimitDatabase
    {
        private static DataSet data = FileHelper.LoadDataSet("\\Database\\WinLossLimit\\WinLossLimit.xml");

        public static DataSet Data => data;
    }
}
