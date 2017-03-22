namespace Alpha.Reports.DataAccesses
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Fanex.Common.Extensions;
    using MemberInfo.AppResources;
    using MemberInfo.Criteria.MemberRequest;
    using MemberInfo.Models;

    public interface MemberRequestDataAccess
    {
        IEnumerable<MemberRequestModel> GetMemberRequest(MemberRequestCriteria criteria);

        Result EnableCasino(EnableCasinoCriteria criteria);
    }

    public class MemberRequestDataAccessImpl : MemberRequestDataAccess
    {
        public IEnumerable<MemberRequestModel> GetMemberRequest(MemberRequestCriteria criteria)
        {
            if (criteria == null)
            {
                throw new ArgumentNullException(nameof(criteria), "Criteria cannot be null");
            }

            var memberRequestData = new List<MemberRequestModel>();

            using (var db = ObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria))
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            memberRequestData.Add(BuildMemberRequest(reader));
                        }
                    }
                }
            }

            return memberRequestData;
        }

        //////// private MemberRequestModel BuildMemberRequest(IDataReader reader)
        ////private MemberRequestModel BuildMemberRequest(int i)
        ////{
        ////    var memberRequest = new MemberRequestModel(
        ////           custId: 125 + (i * 44),
        ////           username: "Username",
        ////           sync: (i % 2 == 1) ? true : false,
        ////           enable: true,
        ////           balance: 45678 + (i * 44), //// reader.ToDecimal("balance"),
        ////           outstanding: 1455 + (i * 44),
        ////           availableCredit: 45662 + (i * 44));

        ////    return memberRequest;
        ////}

         private MemberRequestModel BuildMemberRequest(IDataReader reader)
        {
            var memberRequest = new MemberRequestModel(
                   custId: reader.ToInt("CustId"),
                   username: reader.ToString("Username"),
                   sync: reader.ToBool("IsSync"),
                   balance: reader.ToDecimal("Balance"),
                   outstanding: reader.ToDecimal("OutStanding"),
                   availableCredit: reader.ToDecimal("BetCredit"));

            return memberRequest;
        }

        public Result EnableCasino(EnableCasinoCriteria criteria)
        {
            return Result.CreateErrorResult("enable prouct fail", 150);

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
                        : outputParams["errmsg"].ToString()
                };
            }
        }
    }
}