namespace Alpha.MemberInfo.Features.ParlayMaxPayout
{
    using Alpha.MemberInfo.Models;
    using Criteria;
    using Fanex.Data.Repository;
    using Models;
    using Params;
    using System.Collections.Generic;

    public interface ParlayMaxPayoutService
    {
        IEnumerable<ParlayMaxPayoutModel> GetParlayMaxPayout(ParlayMaxPayoutSelectParam param);

        Result UpdateParlayMaxPayout(ParlayMaxPayoutUpdateParam param);

        Result CheckDownline(ParlayMaxPayoutCheckDownlineParam param);
    }

    public class ParlayMaxPayoutServiceImpl : ParlayMaxPayoutService
    {
        private readonly IDynamicRepository repository;

        public ParlayMaxPayoutServiceImpl(IDynamicRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<ParlayMaxPayoutModel> GetParlayMaxPayout(ParlayMaxPayoutSelectParam param)
            => repository.Fetch<ParlayMaxPayoutModel>(new ParlayMaxPayoutSelectCriteria(param.CustId));

        public Result UpdateParlayMaxPayout(ParlayMaxPayoutUpdateParam param)
            => ExecuteNonQuery(new ParlayMaxPayoutUpdateCriteria(
                param.CustId,
                param.MaxPayoutLimit,
                param.UserId,
                param.SubAccId,
                param.Language,
                param.Ip));

        public Result CheckDownline(ParlayMaxPayoutCheckDownlineParam param)
            => ExecuteNonQuery(new ParlayMaxPayoutCheckDownlineCriteria(
                param.CustId,
                param.MaxPayoutLimit,
                param.Language));

        private Result ExecuteNonQuery(ResultCriteria criteria)
        {
            repository.Execute(criteria);

            return new Result
            {
                Code = criteria.ErrCode,
                Message = criteria.ErrMsg
            };
        }
    }
}