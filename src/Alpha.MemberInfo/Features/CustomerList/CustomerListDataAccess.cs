namespace Alpha.MemberInfo.Features.CustomerList.DataAccess
{
    using Alpha.MemberInfo.Features.CustomerList.Models.AccountInfo;
    using Alpha.MemberInfo.Features.CustomerList.SPs.Criteria;
    using Fanex.Data;
    using System.Data;
    using System.Linq;

    public interface CustomerListDataAccess
    {
        CustomerStatusModel GetCustomerStatus(SelectCustomerAllStatusCriteria criteria);
    }

    public class CustomerListDataAccessImpl : CustomerListDataAccess
    {
        public CustomerStatusModel GetCustomerStatus(SelectCustomerAllStatusCriteria criteria)
        {
            using (var objectDb = ObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
            {
                using (var multiResultReader = objectDb.QueryMultiple(criteria.GetSpParams()))
                {
                    var customerStatus = multiResultReader.Read<NonProductStatusModel>().FirstOrDefault();
                    var nonDCSProduct = multiResultReader.Read<NonDCSProductStatusModel>().FirstOrDefault();
                    var dcsProducts = multiResultReader.Read<DCSProductStatusModel>();

                    return new CustomerStatusModel(
                        customerStatus,
                        nonDCSProduct,
                        dcsProducts
                            .GroupBy(product => product.ProductId)
                            .Select(group => group.First()));
                }
            }
        }
    }
}