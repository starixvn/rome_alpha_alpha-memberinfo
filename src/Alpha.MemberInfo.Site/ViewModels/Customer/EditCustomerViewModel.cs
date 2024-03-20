using Alpha.MemberInfo.Site.Requests.Customer;
using Newtonsoft.Json;

namespace Alpha.MemberInfo.Site.ViewModels.Customer
{
    public class EditCustomerViewModel : CustomerBaseViewModel
    {
        public EditCustomerViewModel(
            EditCustomerQuery query,
            AppConfiguration appConfig,
            string cndUri,
            string assetsVersion)
            : base(appConfig, cndUri, assetsVersion)
        {
            Params = JsonConvert.SerializeObject(new
            {
                custId = query.CustId,
                roleId = query.RoleId,
                uplineRoleId = query.ViewerLevelId,
                productGroupId = query.ProductGroupId
            }, jsonSerializerSettings);
        }
    }

    public class EditMultipleCustomersViewModel : CustomerBaseViewModel
    {
        public EditMultipleCustomersViewModel(EditMultipleCustomersQuery query, AppConfiguration appConfig, string cndUri, string assetsVersion)
            : base(appConfig, cndUri, assetsVersion)
        {
            Params = JsonConvert.SerializeObject(
                new
                {
                    uplineId = query.CustId,
                    uplineRoleId = query.CustLevelId,
                    custIds = query.CustIds,
                    usernames = query.Usernames,
                    roleId = query.RoleId,
                    productGroupId = query.ProductGroupId
                },
                jsonSerializerSettings);
        }
    }
}