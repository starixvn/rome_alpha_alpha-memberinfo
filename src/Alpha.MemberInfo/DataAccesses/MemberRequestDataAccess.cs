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
        IEnumerable<MemberRequest> GetMemberRequest(MemberRequestCriteria criteria);
    }

    public class MemberRequestDataAccessImpl : MemberRequestDataAccess
    {
        public IEnumerable<MemberRequest> GetMemberRequest(MemberRequestCriteria criteria)
        {
            if (criteria == null)
            {
                throw new ArgumentNullException(nameof(criteria), "Criteria cannot be null");
            }

            var memberRequestData = new List<MemberRequest>();

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
        private MemberRequest BuildMemberRequest(IDataReader reader)
        {
            var memberRequest = new MemberRequest(
                   username: "Username",
                   enable: true,
                   balance: reader.ToDecimal("balance"),
                   outstanding: 1455,
                   availableCredit: 45662
                );

            return memberRequest;
        }
    }
}