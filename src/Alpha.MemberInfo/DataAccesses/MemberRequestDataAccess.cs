namespace Alpha.Reports.DataAccesses
{
    using System;
    using MemberInfo.AppResources;
    using MemberInfo.Criteria.MemberRequests;
    using MemberInfo.DataAccesses;
    using MemberInfo.Models;

    public interface MemberRequestDataAccess
    {
        Result EnableCasino(EnableCasinoCriteria criteria);
    }

    public class MemberRequestDataAccessImpl : MemberRequestDataAccess
    {
        public Result EnableCasino(EnableCasinoCriteria criteria)
        {
            if (criteria == null)
            {
                throw new ArgumentNullException(nameof(criteria), "Criteria cannot be null");
            }

            using (var db = ObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
            {
                db.ExecuteNonQuery(criteria.GetSpParams());
                var outputParams = db.GetOuputParams();
                var code = (int)outputParams["errcode"];

                return new Result
                {
                    Code = code,
                    Message = code == 0
                        ? Resources.UpdatedSuccessfully
                        : $"{criteria.CustName} {outputParams["errmsg"].ToString()}"
                };
            }
        }
    }
}