namespace Alpha.MemberInfo.Models.WinLossLimit
{
    public class WinLossLimitProductModel
    {
        public WinLossLimitProductModel()
        {
        }

        public WinLossLimitProductModel(int betType, int sportType, string productName, int productId)
        {
            BetType = betType;
            SportType = sportType;
            ProductName = productName;
            ProductId = productId;
        }

        public int ProductId { get; }

        public int BetType { get; }

        public int SportType { get; }

        public string ProductName { get; }
    }
}
