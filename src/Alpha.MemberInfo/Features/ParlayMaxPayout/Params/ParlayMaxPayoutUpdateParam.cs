namespace Alpha.MemberInfo.Features.ParlayMaxPayout.Params
{
    public class ParlayMaxPayoutUpdateParam
    {
        public ParlayMaxPayoutUpdateParam(
            int custId,
            string maxPayoutLimit,
            int userId,
            string language,
            string ip)
        {
            CustId = custId;
            MaxPayoutLimit = maxPayoutLimit;
            UserId = userId;
            Language = language;
            Ip = ip;
        }

        public int CustId { get; }

        public string MaxPayoutLimit { get; }

        public int UserId { get; }

        public string Language { get; }

        public string Ip { get; }
    }
}