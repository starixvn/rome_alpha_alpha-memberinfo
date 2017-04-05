namespace Alpha.MemberInfo.Models
{
    public class Product
    {
        public Product(byte id, string name, bool isDisable)
        {
            Id = id;
            Name = name;
            IsDisable = isDisable;
        }

        public int Id { get; }

        public string Name { get; }

        public bool IsDisable { get; }
    }
}