namespace Alpha.MemberInfo.Site.Requests
{
    using Enumerations;
    using MemberInfo.Models;

    public class MemberInfoBaseQuery
    {
        private readonly IUser actor;

        internal MemberInfoBaseQuery()
        {
        }

        protected MemberInfoBaseQuery(IUser actor)
        {
            this.actor = actor;
        }

        public int ActorId => actor.Id;

        public int ActorSubAccId => actor.SubAccId;

        public string ActorName => actor.Name;

        public byte ActorLevelId => actor.LevelId;

        public virtual UserLevel ActorLevel => Enumeration.FromValue<UserLevel>(ActorLevelId);

        public string Language => actor.Language;

        public string IP => actor.IP;

        public virtual bool IsValid()
            => ActorId > 0
            && !string.IsNullOrWhiteSpace(ActorName)
            && ActorLevel.IsEqualOrUplineOf(ActorLevel)
            && !string.IsNullOrWhiteSpace(Language);
    }
}