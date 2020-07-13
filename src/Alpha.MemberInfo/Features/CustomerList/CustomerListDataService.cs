namespace Alpha.MemberInfo.Features.CustomerList.Services
{
    using Alpha.MemberInfo.Features.CustomerList.DataAccess;
    using Alpha.MemberInfo.Features.CustomerList.Models.AccountInfo;
    using Alpha.MemberInfo.Features.CustomerList.SPs.Criteria;
    using Fanex.Data.Repository;

    public interface CustomerListDataService
    {
       CustomerStatusModel GetCustomerStatus(int custId);
    }

    public class CustomerListDataServiceImpl : CustomerListDataService
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly CustomerListDataAccess customerListDataAccess;

        public CustomerListDataServiceImpl(
            IDynamicRepository dynamicRepository,
            CustomerListDataAccess customerListDataAccess)
        {
            this.dynamicRepository = dynamicRepository;
            this.customerListDataAccess = customerListDataAccess;
        }

        public CustomerStatusModel GetCustomerStatus(int custId)
           => customerListDataAccess.GetCustomerStatus(new SelectCustomerAllStatusCriteria(custId));
    }
}