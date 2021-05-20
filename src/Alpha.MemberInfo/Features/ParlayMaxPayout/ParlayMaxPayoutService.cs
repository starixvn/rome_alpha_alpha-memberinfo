namespace Alpha.MemberInfo.Features.ParlayMaxPayout
{
    using Alpha.MemberInfo.Models;
    using Criteria;
    using Models;
    using Params;
    using System.Collections.Generic;

    public interface ParlayMaxPayoutService
    {
        IEnumerable<ParlayMaxPayoutModel> GetParlayMaxPayout(ParlayMaxPayoutSelectParam param);

        Result UpdateParlayMaxPayout(ParlayMaxPayoutUpdateParam param);
    }

    public class ParlayMaxPayoutServiceImpl : ParlayMaxPayoutService
    {
        private readonly ParlayMaxPayoutDataAccess dataAccess;

        public ParlayMaxPayoutServiceImpl(ParlayMaxPayoutDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public IEnumerable<ParlayMaxPayoutModel> GetParlayMaxPayout(ParlayMaxPayoutSelectParam param)
            => dataAccess.GetParlayMaxPayout(new ParlayMaxPayoutSelectCriteria(param.CustId));

        public Result UpdateParlayMaxPayout(ParlayMaxPayoutUpdateParam param)
            => dataAccess.UpdateParlayMaxPayout(new ParlayMaxPayoutUpdateCriteria(
                param.CustId,
                param.MaxPayoutLimit,
                param.SubAccId,
                param.UserId,
                param.Ip));
    }
}