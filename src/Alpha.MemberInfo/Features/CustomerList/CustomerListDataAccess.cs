namespace Alpha.MemberInfo.Features.CustomerList
{
    using Alpha.MemberInfo.Features.CustomerList.Criteria;
    using Alpha.MemberInfo.Features.CustomerList.Models;
    using Alpha.MemberInfo.Models;
    using Fanex.Data;
    using System.Data;
    using System.Linq;

    public interface CustomerListDataAccess
    {
        CustomerStatusModel GetCustomerStatus(SelectCustomerAllStatusModelCriteria criteria);

        Result UpdateCustomerStatus(UpdateCustomerStatusCommand command);
    }

    public class CustomerListDataAccessImpl : CustomerListDataAccess
    {
        public CustomerStatusModel GetCustomerStatus(SelectCustomerAllStatusModelCriteria criteria)
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
                            .Select(products =>
                            {
                                foreach (var product in products)
                                {
                                    if (product.IsDisabled)
                                    {
                                        return product;
                                    }
                                }

                                return products.First();
                            }));
                }
            }
        }

        public Result UpdateCustomerStatus(UpdateCustomerStatusCommand command)
        {
            using (var objectDb = ObjectDbFactory.CreateInstance(command.GetSettingKey()))
            {
                objectDb.ExecuteNonQuery(command.GetSpParams());

                var outputParams = objectDb.GetOuputParams();

                return new Result
                {
                    Code = int.Parse(outputParams["errcode"].ToString()),
                    Message = outputParams["errmsg"]?.ToString()
                };
            }
        }
    }
}