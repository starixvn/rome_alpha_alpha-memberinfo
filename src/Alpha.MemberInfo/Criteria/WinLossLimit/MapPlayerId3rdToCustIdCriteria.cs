namespace Alpha.MemberInfo.Criteria.WinLossLimit
{
    using Fanex.Data.Repository;

    public class MapPlayerId3rdToCustIdCriteria : CriteriaBase
    {
        public MapPlayerId3rdToCustIdCriteria(int productId, string playerId)
        {
            ProductId = productId;
            PlayerId = playerId ?? string.Empty;
        }

        public int ProductId { get; }

        public string PlayerId { get; }

        public int CustId { get; set; }

        public override string GetSettingKey()
            => "Acc_CustomerSelByPlayerId";

        public override bool IsValid()
            => true;
    }
}
