namespace Alpha.MemberInfo.Features.CustomerList.Services
{
    using Fanex.Data.Repository;

    public interface CustomerListDataService
    {
    }

    public class CustomerListDataServiceImpl : CustomerListDataService
    {
        private readonly IDynamicRepository dynamicRepository;

        public CustomerListDataServiceImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }
    }
}