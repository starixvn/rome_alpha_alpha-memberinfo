namespace Alpha.MemberInfo.Models
{
    using System;

    public class Product
    {
        public Product(int id, string name, bool disabled)
        {
            Id = id;
            Name = name;
            Disabled = disabled;
        }

        internal Product(int id, string name, bool disabled, Func<int, string> getProductDisplayName) : this(id, name, disabled)
        {
            DisplayName = getProductDisplayName == null ? name : getProductDisplayName(id);
        }

        public bool Disabled { get; }

        public int Id { get; }

        public string Name { get; }

        public string DisplayName { get; }
    }
}