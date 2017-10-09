namespace Alpha.MemberInfo.Models
{
    public interface IUser
    {
        int Id { get; }

        string Name { get; }

        string Language { get; }

        string LanguageKey { get; }

        byte LevelId { get; }

        int SubAccId { get; }

        string SubAccName { get; }

        string IP { get; }

        bool CanUpdateAccount { get; }

        bool GetHideProduct(int productId);

        bool GetSyncedProduct(int productId);
    }
}