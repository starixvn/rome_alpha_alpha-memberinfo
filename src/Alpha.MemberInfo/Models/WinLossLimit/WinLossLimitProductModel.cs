namespace Alpha.MemberInfo.Models.WinLossLimit
{
    public class WinLossLimitProductModel
    {
        public WinLossLimitProductModel()
        {
        }

        public WinLossLimitProductModel(int betType, int sportType, string productName, int productId, string playerId = null)
        {
            BetType = betType;
            SportType = sportType;
            ProductName = productName;
            ProductId = productId;
            PlayerId = playerId ?? "-";
        }

        public int ProductId { get; }

        public int BetType { get; }

        public int SportType { get; }

        public string ProductName { get; set; }

        public string PlayerId { get; }
    }
}
