namespace Alpha.MemberInfo.Site.Features.ParlayMaxPayout.Requests
{
    using Alpha.MemberInfo.Models;
    using MediatR;
    using System.Collections.Generic;

    public class ParlayMaxPayoutUpdateBaseQuery
    {
        public ParlayMaxPayoutUpdateBaseQuery(IUser viewer)
        {
            SubAccId = viewer.SubAccId;
            UserId = viewer.Id;
            Ip = viewer.IP;
        }

        public string CustIdList { get; set; }

        public string MaxPayoutLimit { get; set; }

        public int SubAccId { get; }

        public int UserId { get; }

        public string Ip { get; }
    }

    public class ParlayMaxPayoutUpdateSingleQuery :
        ParlayMaxPayoutUpdateBaseQuery, IRequest<Result>
    {
        public ParlayMaxPayoutUpdateSingleQuery(IUser viewer) : base(viewer)
        {
        }

        public int CustId
        {
            get
            {
                int custId;
                int.TryParse(CustIdList, out custId);
                return custId;
            }
        }
    }

    public class ParlayMaxPayoutUpdateMultipleQuery :
        ParlayMaxPayoutUpdateBaseQuery, IRequest<IEnumerable<Result>>
    {
        public ParlayMaxPayoutUpdateMultipleQuery(IUser viewer) : base(viewer)
        {
        }
    }
}