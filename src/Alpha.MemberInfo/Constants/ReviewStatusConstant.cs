using System.Collections;
using System.Collections.Generic;

namespace Alpha.MemberInfo.Constants
{
    public static class ReviewStatusConstant
    {
        public const int All = 0;
        public const int Reviewed = 1;
        public const int NotReviewed = 2;

        public static readonly IDictionary<int, string> List = new Dictionary<int, string>
        {
            [All] = nameof(All),
            [Reviewed] = nameof(Reviewed),
            [NotReviewed] = nameof(NotReviewed)
        };

        public static string GetReviewStatus(int key)
            => List.ContainsKey(key) ? List[key] : List[All];
    }
}
