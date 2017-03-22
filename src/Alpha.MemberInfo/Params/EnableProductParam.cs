namespace Alpha.MemberInfo.Params
{
    public class EnableProductParam
    {
        public EnableProductParam(
         string membersInfo,
         int actorId,
         string actorName,
         int subAccId,
         string loginIP,
         string language)
        {
            MembersInfo = membersInfo;
            ActorId = actorId;
            ActorName = actorName;
            SubAccId = subAccId;
            LoginIP = loginIP;
            Language = language;
        }

        public string MembersInfo { get; }

        public int ActorId { get; }

        public string ActorName { get; }

        public int SubAccId { get; }

        public string LoginIP { get; }

        public string Language { get; }
    }
}