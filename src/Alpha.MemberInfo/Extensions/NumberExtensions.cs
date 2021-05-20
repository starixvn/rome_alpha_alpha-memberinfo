namespace Alpha.MemberInfo.Extensions
{
    using System.Globalization;

    public static class NumberExtensions
    {
        private const string moneyFormat = "#,##0.##";
        private const string moneyWithZeroTailFormat = "#,##0.00";

        public static string ToMoneyFormat(this decimal number)
            => number.ToString(moneyFormat, CultureInfo.InvariantCulture);

        public static string ToMoneyFormatWithZeroTail(this decimal number)
            => number.ToString(moneyWithZeroTailFormat, CultureInfo.InvariantCulture);
    }
}