namespace Alpha.MemberInfo.Features.MyMonitorAccounts.ReachedThreshold.Criteria
{
    using Fanex.Data.Repository;
    using System;

    public class SelectReachListCriteria : CriteriaBase
    {
        public SelectReachListCriteria(
            int agentId, 
            string username, 
            DateTime fromDate, 
            DateTime toDate, 
            string status, 
            byte limitType, 
            byte sortType, 
            bool isDesc,
            int pageSize,
            int pageIndex,
            int sporttype = 0)
        {
            AgentId = agentId;
            Username = username;
            FromDate = fromDate;
            ToDate = toDate;
            Status = status;
            LimitType = limitType;
            SortType = sortType;
            IsDesc = isDesc;
            PageSize = pageSize;
            PageIndex = pageIndex;
            Sporttype = sporttype;
        }

        public int AgentId { get; }

        public string Username { get; }

        public DateTime FromDate { get; }

        public DateTime ToDate { get; }

        public string Status { get; }

        public byte LimitType { get; }

        public byte SortType { get; }

        public bool IsDesc { get; }

        public int Sporttype { get; }

        public int PageIndex { get; }

        public int PageSize { get; }

        public int TotalItems { get; set; }

        public override string GetSettingKey() => "Acc_WLMonitored_Player_ReachList_Sel";

        public override bool IsValid() => AgentId > 0 && FromDate < ToDate;
    }
}
