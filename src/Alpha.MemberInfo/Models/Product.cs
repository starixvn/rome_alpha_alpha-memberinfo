namespace Alpha.MemberInfo.Models
{
    using System;

    public class Product
    {
        public Product()
        {
        }

        public Product(int id, string name, bool isDisable)
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