namespace Alpha.MemberInfo.Site.Requests
{
    using Enumerations;
    using MemberInfo.Models;

    public class MemberInfoBaseQuery
    {
        internal MemberInfoBaseQuery()
        {
        }

        protected MemberInfoBaseQuery(IUser viewer)
        {
            // Default data, if query string had data, then get from query string.
            CustId = viewer.Id;
            CustName = viewer.Name;
            CustLevelId = viewer.LevelId;

            Language = viewer.Language;

            ViewerId = viewer.Id;
            ViewerLevelId = viewer.LevelId;
            ViewerLevel = Enumeration.FromValue<UserLevel>(ViewerLevelId);
        }

        public string Language { get; }

        public int CustId { get; set; }

        public string CustName { get; set; }

        public byte CustLevelId { get; set; }

        public virtual UserLevel CustLevel => Enumeration.FromValue<UserLevel>(CustLevelId);

        public int ViewerId { get; }

        public byte ViewerLevelId { get; }

        public virtual UserLevel ViewerLevel { get; }

        public virtual bool IsValid()
            => CustId > 0
            && !string.IsNullOrWhiteSpace(CustName)
            && ViewerId > 0
            && ViewerLevel.IsEqualOrUplineOf(CustLevel)
            && !string.IsNullOrWhiteSpace(Language);
    }
}