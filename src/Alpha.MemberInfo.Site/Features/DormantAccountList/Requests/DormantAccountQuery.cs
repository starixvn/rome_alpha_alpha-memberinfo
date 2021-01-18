namespace Alpha.MemberInfo.Site.Features.DormantAccountList.Requests
{
    using System;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.DormantAccountList.ViewModels;
    using Alpha.MemberInfo.Site.Requests;
    using MediatR;

    public class DormantAccountQuery : MemberInfoBaseQuery, IAsyncRequest<DormantAccountViewModel>
    {
        public DormantAccountQuery(IUser viewer) : base(viewer)
        {
            LimitedDateToShowInfo = viewer.LimitedDateToShowInfo;
        }

        public string Username { get; set; }

        public byte LevelId { get; set; }

        public byte StatusId { get; set; }

        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 500;

        public DateTime LimitedDateToShowInfo { get; }
    }
}