namespace Alpha.MemberInfo.Params
{
    public class EnableProductParam
    {
        public EnableProductParam(
         string custIds,
         string username,
         int actorId,
         string actorName,
         int subAccId,
         string loginIP,
         string language)
        {
            CustIds = custIds;
            Username = username;
            ActorId = actorId;
            ActorName = actorName;
            SubAccId = subAccId;
            LoginIP = loginIP;
            Language = language;
        }

        public string CustIds { get; }

        public string Username { get; }

        public int ActorId { get; }

        public string ActorName { get; }

        public int SubAccId { get; }

        public string LoginIP { get; }

        public string Language { get; }
    }
}