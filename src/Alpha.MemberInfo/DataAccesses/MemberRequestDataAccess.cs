namespace Alpha.Reports.DataAccesses
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Criteria.WinLoss;
    using Fanex.Common.Extensions;
    using MemberInfo.Models;

    public interface MemberRequestDataAccess
    {
        IEnumerable<MemberRequestModel> GetMemberRequest(MemberRequestCriteria criteria);
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
    }
}