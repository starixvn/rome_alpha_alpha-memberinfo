namespace Alpha.MemberInfo.Site.Features.CreateAccount
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Alpha.MemberInfo.Features.CreateAccount;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Services;
    using Alpha.Web;
    using Models;
    using Alpha.MemberInfo.Configurations;
    using System.Collections.Generic;
    using Alpha.MemberInfo.Features.CreateAccount.Models;

    public class ProductSettingInfoController : BaseController
    {
        private readonly IUser viewer;
        private readonly IProductSettingInfoService service;
        private readonly CacheRefService refService;

        private const string ProductSettingInformationViewPath =
            "~/Features/CreateAccount/Views/ProductSettingInfo.cshtml";
        
        private const int RNGGameGroupdId = 2;
        private const int LiveRNGCasinoGroupdId = 3;
        private const int BBINProductID = 47;
        private const int NumberGameProductID = 3;
        private const int RNGKENOProductID = 8;

        public ProductSettingInfoController(IUser viewer, IProductSettingInfoService service,
            CacheRefService refService)
        {
            this.viewer = viewer;
            this.service = service;
            this.refService = refService;
        }

        public ActionResult Index() => View(ProductSettingInformationViewPath);

        [HttpGet]
        public async Task<ActionResult> GetProductSettingInfo()
        {
            var products = await service.GetProductsSettingInfo(viewer.Id);
            
            var groupedData = products
                .GroupBy(d => d.GroupId)
                .Select(group => new
                {
                    Id = group.Key,
                    Name = refService.GetProductGroupName(group.Key),
                    Products = (group.GroupBy(d => d.ProductId)
                        .Select(productGroup => new
                        {
                            Id = productGroup.Key,
                            Name = refService.GetProductName(productGroup.Key),
                            Products =(productGroup.Select(d => new
                            {
                                Name = refService.GetProductName(d.RealProductId),
                                Products = ""
                            }).ToList())
                        })
                        .ToList())
                })
                .ToList();
            
            var jsonResponse = new
            {
                ProductSettingInfo = groupedData,
                RoleId = viewer.LevelId,
                Is789y = AppSettings.IsSiteLabel789y,
                ShowBBin = products
                    .Where(gr => gr.GroupId == LiveRNGCasinoGroupdId)
                    .Any(e => e.RealProductId == BBINProductID),
                ShowRNGKenoNumberGame = products
                    .Where(gr => gr.GroupId == RNGGameGroupdId)
                    .Any(e => e.RealProductId == NumberGameProductID || e.RealProductId == RNGKENOProductID),
            };

            return Json(jsonResponse, JsonRequestBehavior.AllowGet);
        }
    }
}