namespace Alpha.MemberInfo.Params.IndirectLoginAccount
{
    public class RedirectParam
    {
        public RedirectParam(
            int indirectCustId,
            int indirectSubAccId,
            string language,
            int actorId,
            int actorSubAccId,
            string actorUsername,
            string browserSize,
            string ibcPKey)
        {
            IndirectCustId = indirectCustId;
            IndirectSubAccId = indirectSubAccId;
            Language = language;
            ActorId = actorId;
            ActorSubAccId = actorSubAccId;
            ActorUsername = actorUsername;
            BrowserSize = browserSize;
            IBCPKey = ibcPKey;
        }

        public int IndirectCustId { get; }

        public int IndirectSubAccId { get; }

        public string Language { get; }

        public int ActorId { get; }

        public int ActorSubAccId { get; }

        public string ActorUsername { get; }

        public string BrowserSize { get; }

        public string IBCPKey { get; }
    }
}
