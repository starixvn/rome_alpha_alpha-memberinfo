namespace Alpha.MemberInfo.Features.ParlayMaxPayout
{
    using Alpha.MemberInfo.Models;
    using Criteria;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using Models;
    using System.Collections.Generic;

    public interface ParlayMaxPayoutDataAccess
    {
        IEnumerable<ParlayMaxPayoutModel> GetParlayMaxPayout(ParlayMaxPayoutSelectCriteria criteria);

        Result UpdateParlayMaxPayout(ParlayMaxPayoutUpdateCriteria criteria);
    }

    public class ParlayMaxPayoutDataAccessImpl : ParlayMaxPayoutDataAccess
    {
        private readonly IDynamicRepository repository;

        public ParlayMaxPayoutDataAccessImpl(IDynamicRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<ParlayMaxPayoutModel> GetParlayMaxPayout(ParlayMaxPayoutSelectCriteria criteria)
            => repository.Fetch<ParlayMaxPayoutModel>(criteria);

        public Result UpdateParlayMaxPayout(ParlayMaxPayoutUpdateCriteria criteria)
        {
            using (var db = ObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
            {
                db.ExecuteNonQuery(criteria.GetSpParams());

                var outputParams = db.GetOuputParams();

                return new Result
                {
                    Code = int.Parse(outputParams["errcode"].ToString()),
                    Message = outputParams["errmsg"]?.ToString()
                };
            }
        }
    }
}