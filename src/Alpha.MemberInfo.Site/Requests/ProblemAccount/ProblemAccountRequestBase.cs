namespace Alpha.MemberInfo.Site.Requests.ProblemAccount
{
    using System;
    using System.Web.Mvc;
    using Alpha.MemberInfo.Constants;
    using Alpha.MemberInfo.Models;

    public class ProblemAccountRequestBase : MemberInfoBaseQuery
    {
        public ProblemAccountRequestBase(IUser viewer) : base(viewer)
        {
            FromDate = DateTime.Now.AddDays(-13);
            ToDate = DateTime.Now;
            UserSelectedReviewedStatusId = ReviewStatusConstant.All;
        }

        protected readonly byte MemberRoleId = UserLevel.Member.Value;

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int UserSelectedReviewedStatusId { get; set; }

        public int UplineId { get; set; }

        [AllowHtml]
        public string UplineName { get; set; }

        public byte SearchRoleId => MemberRoleId;

        [AllowHtml]
        public string UserName { get; set; }

        public override bool IsValid()
        {
            return base.IsValid() && ActorLevel.IsSuperLevel();
        }
    }
}