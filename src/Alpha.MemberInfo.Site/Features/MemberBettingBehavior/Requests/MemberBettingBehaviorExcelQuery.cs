namespace Alpha.MemberInfo.Site.Features.MemberBettingBehavior.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Requests;
    using Alpha.MemberInfo.Site.ViewModels;
    using MediatR;
    using System;

    public class MemberBettingBehaviorExcelQuery : MemberInfoBaseQuery, IAsyncRequest<ExcelFileViewModel>
    {
        public MemberBettingBehaviorExcelQuery(IUser viewer) : base(viewer)
        {
        }
        public int MemberCustId { get; set; }

        public string UserName { get; set; }

        public int DateRange { get; set; }

        public DateTime EndState => DateTime.Now.AddDays(-2);

        public DateTime StartDate
        {
            get
            {
                var range = DateRange > 90 ? 90 : DateRange;
                return EndState.AddDays(-range) > viewer.LimitedDateToShowInfo ? EndState.AddDays(-range) : viewer.LimitedDateToShowInfo;
            }
        }
    }
}