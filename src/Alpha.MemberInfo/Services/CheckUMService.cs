namespace Alpha.MemberInfo.Services
{
    using Criteria;
    using DataAccesses;
    using Models;
    using Params;

    public interface CheckUMService
    {
        Result CheckUM(CheckUMParam param);
    }

    public class CheckUMServiceImpl : CheckUMService
    {
        private readonly CacheRefService refService;
        private readonly CheckUMDataAccess dataAccess;

        public CheckUMServiceImpl(CacheRefService refService, CheckUMDataAccess dataAccess)
        {
            this.refService = refService;
            this.dataAccess = dataAccess;
        }

        public Result CheckUM(CheckUMParam param)
        {
            var result = dataAccess.CheckUM(new CheckUMCriteria(param.CustId, param.ProductId, param.Language));

            result.Message = string.Format(result.Message, refService.GetProductName(param.ProductId));

            return result;
        }
    }
}