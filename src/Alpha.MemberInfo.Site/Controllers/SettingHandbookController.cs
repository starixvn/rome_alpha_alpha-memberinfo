using System.Web.Mvc;
using Alpha.MemberInfo.Site.Requests.Customer;
using Alpha.Web;

namespace Alpha.MemberInfo.Site.Controllers
{
    public class SettingHandbookController : BaseController
    {
        public ActionResult SportBookInformation(SportBookInformationQuery query)
        {
            var url = "~/Customer/SportBookInformation?tabindex=" + query.TabIndex
                + "&custId=" + query.CustId
                + "&roleId=" + query.RoleId
                + "&site=" + query.Site
                + "&siteLabel=" + query.SiteLabel
                + "&currency=" + query.Currency;
            return Redirect(url);
        }
    }
}