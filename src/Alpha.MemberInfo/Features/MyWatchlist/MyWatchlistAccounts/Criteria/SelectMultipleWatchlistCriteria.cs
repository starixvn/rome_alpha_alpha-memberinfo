//namespace Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Criteria
//{
//    using Fanex.Data.Repository;

//    public class SelectMultipleWatchlistCriteria : CriteriaBase 
//    {
//        public SelectMultipleWatchlistCriteria(string custIds)
//            => CustList = custIds;

//        public string CustList { get; }

//        public override string GetSettingKey() => "Acc_WLMonitored_CheckExistSettingByCust";

//        public override bool IsValid() => !string.IsNullOrWhiteSpace(CustList);
//    }
//}
