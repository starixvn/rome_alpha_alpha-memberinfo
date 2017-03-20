namespace Alpha.Reports.DataAccesses
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Criteria.MemberRequest;
    using Fanex.Common.Extensions;
    using MemberInfo.AppResources;
    using MemberInfo.Models;

    public interface MemberRequestDataAccess
    {
        IEnumerable<MemberRequestModel> GetMemberRequest(MemberRequestCriteria criteria);

        IEnumerable<Result> EnableProduct(EnableProductCriteria criteria);

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

            for (int i = 0; i < 5; i++)
            {
                memberRequestData.Add(BuildMemberRequest(i));
            }

            ////using (var db = ObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
            ////{
            ////    using (var reader = db.ExecuteReader(criteria))
            ////    {
            ////        if (reader != null)
            ////        {
            ////            while (reader.Read())
            ////            {
            ////                memberRequestData.Add(BuildMemberRequest(reader));
            ////            }
            ////        }
            ////    }
            ////}

            return memberRequestData;
        }

        public IEnumerable<Result> EnableProduct(EnableProductCriteria criteria)
        {
            throw new NotImplementedException();
        }

        //// private MemberRequestModel BuildMemberRequest(IDataReader reader)
        private MemberRequestModel BuildMemberRequest(int i)
        {
            var memberRequest = new MemberRequestModel(
                   username: "Username",
                   enable: true,
                   balance: 45678 + (i * 44), //// reader.ToDecimal("balance"),
                   outstanding: 1455 + (i * 44),
                   availableCredit: 45662 + (i * 44));

            return memberRequest;
        }

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
                        : outputParams["errmsg"].ToString()
                };
            }
        }
    }
}