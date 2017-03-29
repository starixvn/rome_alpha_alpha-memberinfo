namespace Alpha.MemberInfo.Criteria
{
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class InsertPersonalMessageCriteria : CriteriaBase
    {
        public InsertPersonalMessageCriteria(string content, int type)
        {
            ContentEN = content;
            ContentCH = content;
            ContentCS = content;
            ContentJP = content;
            ContentIT = string.Empty;
            ContentTH = content;
            ContentKO = content;
            ContentES = string.Empty;
            Type = type;
            IsSticky = false;
            MatchId = 0;
            IsBatch = true;

            Creator = 0;
            IsTest = false;
            CreatorName = string.Empty;
        }

        [SpParam(SqlParamName = "content_en")]
        public string ContentEN { get; }

        [SpParam(SqlParamName = "content_ch")]
        public string ContentCH { get; }

        [SpParam(SqlParamName = "content_cs")]
        public string ContentCS { get; }

        [SpParam(SqlParamName = "content_jp")]
        public string ContentJP { get; }

        [SpParam(SqlParamName = "content_it")]
        public string ContentIT { get; }

        [SpParam(SqlParamName = "content_th")]
        public string ContentTH { get; }

        [SpParam(SqlParamName = "content_ko")]
        public string ContentKO { get; }

        [SpParam(SqlParamName = "content_es")]
        public string ContentES { get; }

        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "Reviewed!")]
        [SpParam(SqlParamName = "msgtype")]
        public int Type { get; }

        public bool IsSticky { get; }

        public int Creator { get; }

        public string CreatorName { get; }

        public bool IsTest { get; }

        public int MatchId { get; }

        public bool IsBatch { get; }

        public override string GetSettingKey() => "Dot_Msg_SetMessage";

        public override bool IsValid() => true;
    }
}