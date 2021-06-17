namespace Alpha.MemberInfo.Site.Features.ParlayMaxPayout.Requests
{
    using Alpha.MemberInfo.Models;
    using MediatR;
    using System.Collections.Generic;

    public class ParlayMaxPayoutUpdateBaseQuery
    {
        public ParlayMaxPayoutUpdateBaseQuery(IUser viewer)
        {
            UserId = viewer.Id;
            SubAccId = viewer.SubAccId;
            Language = viewer.Language;
            Ip = viewer.IP;
            CanUpdateAccount = viewer.CanUpdateAccount;
        }

        public string CustIdList { get; set; }

        public string MaxPayoutLimit { get; set; }

        public int UserId { get; }

        public int SubAccId { get; }

        public string Language { get; }

        public string Ip { get; }

        public bool CanUpdateAccount { get; }
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