namespace Alpha.MemberInfo.Features.ParlayMaxPayout.Params
{
    public class ParlayMaxPayoutUpdateParam
    {
        public ParlayMaxPayoutUpdateParam(
            int custId,
            string maxPayoutLimit,
            int subAccId,
            int userId,
            string ip)
        {
            CustId = custId;
            MaxPayoutLimit = maxPayoutLimit;
            SubAccId = subAccId;
            UserId = userId;
            Ip = ip;
        }

        public int CustId { get; }

        public string MaxPayoutLimit { get; }

        public int SubAccId { get; }

        public int UserId { get; }

        public string Ip { get; }
    }
}