namespace Alpha.MemberInfo.Models
{
    public interface IUser
    {
        int Id { get; }

        string Name { get; }

        string Language { get; }

        byte LevelId { get; }

        int SubAccId { get; }

        string IP { get; }

        bool GetHideProduct(int productId);
    }
}