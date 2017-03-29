namespace Alpha.MemberInfo.Criteria
{
    using Fanex.Data.Repository;

    public class InsertPersonalMessageTargetCriteria : CriteriaBase
    {
        public InsertPersonalMessageTargetCriteria(
            long messageId,
            int targetId,
            string targetName,
            bool isRead,
            string betTransId)
        {
            MessageId = messageId;
            TargetId = targetId;
            TargetName = targetName;
            IsRead = isRead;
            BetTransId = betTransId;
        }

        public long MessageId { get; }

        public int TargetId { get; }

        public string TargetName { get; }

        public bool IsRead { get; }

        public string BetTransId { get; }

        public override string GetSettingKey() => "Dot_Msg_SetUsrMessage";

        public override bool IsValid() => MessageId > 0;
    }
}