namespace Alpha.MemberInfo.Helpers
{
    using Constants;

    public static class ProductHelper
    {
        public static bool IsPokerGames(this int productId) => productId == ProductId.PokerGames;
    }
}
