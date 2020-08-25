namespace Alpha.MemberInfo.Site.Features.CustomerList.ViewModels
{
    public class UnsyncedProductModel
    {
        public UnsyncedProductModel(
            int id,
            string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }

        public string Name { get; }
    }
}