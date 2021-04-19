namespace Alpha.MemberInfo.Site.Features.CustomerList.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.Shared.Requests;
    using System;

    public class FetchDownlinesListRequesBase : BaseRequest
    {
        public FetchDownlinesListRequesBase(IUser viewer) : base(viewer)
        {
            DoubleComm = 2;
            LimitedDateToShowInfo = viewer.LimitedDateToShowInfo != default(DateTime);
            IsShowMyanmarOdds = viewer.IsShowMyanmarOdds;
            CanViewReports = viewer.CanViewReports;
        }

        public string Username { get; set; }

        public byte Status { get; set; }

        public byte DoubleComm { get; set; }

        public byte UnconfirmSetting { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public bool? Suspended
           => Status.Equals(2) ? (bool?)true : null;

        public bool? Enabled
             => Status.Equals(4) ? (bool?)false : null;

        public bool? Closed
            => Status.Equals(1) || Status.Equals(3) ? (bool?)Status.Equals(3) : null;

        public bool Locked
            => Status.Equals(5);

        public bool LimitedDateToShowInfo { get; }

        public bool IsShowMyanmarOdds { get; }

        public bool ShowNickname => ViewerLevelId == CustLevelId;

        public bool CanViewReports { get; }
    }
}