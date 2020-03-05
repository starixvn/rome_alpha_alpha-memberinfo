using System;

namespace Alpha.MemberInfo.Extensions
{
    public static class DateTimeExtensions
    {
        private const string ShortDateWithSlash = "MM/dd/yyyy";
        private const string LongDateTime24h = "MM/dd/yyyy HH:mm";
        private const string ShortDateWithHyphen = "yyyy-MM-dd";


        public static string ToShortDateWithHyphen(this DateTime dateTime)
            => dateTime.ToString(ShortDateWithHyphen);

        public static string ToShortDateWithSlash(this DateTime dateTime)
            => dateTime.ToString(ShortDateWithSlash);

        public static string ToLongDateTime24h(this DateTime dateTime)
            => dateTime.ToString(LongDateTime24h);
    }
}
