namespace Alpha.MemberInfo.Features.CustomerList
{
    using System;
    using System.Data;
    using System.Linq;
    using Alpha.MemberInfo.Features.CustomerList.Criteria;
    using Alpha.MemberInfo.Features.CustomerList.Models;
    using Alpha.MemberInfo.Features.CustomerList.SPs.Criteria;
    using Alpha.MemberInfo.Models;
    using Fanex.Data;
    using Fanex.Data.Repository;

    public interface CustomerListDataAccess
    {
        CustomerStatusModel GetCustomerStatus(SelectCustomerAllStatusModelCriteria criteria);

        Result UpdateCustomerStatus(NonQueryCommand command);

        bool GetIsProtectedMode(SelectClosedReasonCriteria criteria);
    }

    public class CustomerListDataAccessImpl : CustomerListDataAccess
    {
        public CustomerStatusModel GetCustomerStatus(SelectCustomerAllStatusModelCriteria criteria)
        {
            using (var objectDb = ObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
            {
                using (var multiResultReader = objectDb.QueryMultiple(criteria))
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
                                    if (!product.IsHidden)
                                    {
                                        return product;
                                    }
                                }

                                return products.First();
                            }));
                }
            }
        }

        public Result UpdateCustomerStatus(NonQueryCommand command)
        {   

            using (var objectDb = ObjectDbFactory.CreateInstance(command.GetSettingKey()))
            {
                objectDb.ExecuteNonQuery(command);

                var outputParams = objectDb.GetOutputParams();

                return new Result
                {
                    Code = int.Parse(outputParams["errcode"].ToString()),
                    Message = outputParams["errmsg"]?.ToString()
                };
            }
        }

        public bool GetIsProtectedMode(SelectClosedReasonCriteria criteria)
        {
            using (var db = ObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    if (reader.Read())
                    {
                        var isProtectedMode = reader["IsProtectedMode"];

                        return isProtectedMode == null || isProtectedMode == DBNull.Value
                            ? false
                            : Convert.ToBoolean(isProtectedMode);
                    }
                }
            }

            return false;
        }

         
    }
}