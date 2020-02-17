namespace Alpha.MemberInfo.Site.Requests.ProblemAccount
{
    using System;
    using Alpha.MemberInfo.Constants;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.ViewModels.ProblemAccount;
    using MediatR;

    public class IndexRequest: MemberInfoBaseQuery, IRequest<ProblemAccountReportViewModel>
    {
        public IndexRequest(IUser viewer):base(viewer)
        {
            FromDate = DateTime.Now.AddDays(-13);
            ToDate = DateTime.Now;
            UserSelectedReviewedStatusId = ReviewStatusConstant.NotReviewed;
            SearchRoleId = UserLevel.Master.Value;
        }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int UserSelectedReviewedStatusId { get; set; }

        public int UplineId { get; set; }

        public string UplineName { get; set; }

        public byte SearchRoleId { get; set; }

        public string UserName { get; set; }
    }
}