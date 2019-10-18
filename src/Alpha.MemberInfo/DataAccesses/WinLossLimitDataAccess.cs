namespace Alpha.MemberInfo.DataAccesses
{
    using System;
    using System.Collections.Generic;
    using Criteria.WinLossLimit;
    using Fanex.Common;
    using Models.WinLossLimit;

    public interface WinLossLimitDataAccess
    {
        WinLossLimitPage GetWinLossLimits(GetCustWinLossLimitCriteria criteria);

        int MapPlayerIdToCustId(MapPlayerId3rdToCustIdCriteria criteria);
    }

    public class WinLossLimitDataAccessImpl : WinLossLimitDataAccess
    {
        public WinLossLimitPage GetWinLossLimits(GetCustWinLossLimitCriteria criteria)
        {
            if (!criteria.IsValid())
            {
                return new WinLossLimitPage();
            }

            var winLossLimitDetail = new List<WinLossLimitDetail>();
            var numberOfRows = 0;

            using (var objectDb = MemberInfoObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
            {
                using (var reader = objectDb.ExecuteReader(criteria))
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            winLossLimitDetail.Add(new WinLossLimitDetail
                            {
                                CustId = Converter.ToInt(reader["CustId"]),
                                PlayerId = string.Empty,
                                Closed = Converter.ToBool(reader["Closed"]),
                                Enabled = Converter.ToBool(reader["Enabled"]),
                                Suspended = Converter.ToBool(reader["Suspended"]),
                                Username = Converter.ToString2(reader["Username"]),
                                Firstname = Converter.ToString2(reader["Firstname"]),
                                Lastname = Converter.ToString2(reader["Lastname"]),
                                Credit = Converter.ToDecimal(reader["Credit"]),
                                IsAutoReset = Converter.ToBool(reader["IsAutoReset"]),
                                MaxWinLimit = Converter.ToDecimal(reader["MaxWinLimit"]),
                                MaxLossLimit = reader["MaxLossLimit"] == DBNull.Value ? (decimal?)null : Converter.ToDecimal(reader["MaxLossLimit"])
                            });
                        }

                        reader.Close();
                    }

                    numberOfRows = Converter.ToInt(objectDb.GetOuputParams()["totalrows"]);
                }
            }

            return new WinLossLimitPage(numberOfRows, winLossLimitDetail);
        }

        public int MapPlayerIdToCustId(MapPlayerId3rdToCustIdCriteria criteria)
        {
            var custId = 0;

            using (var objectDb = MemberInfoObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
            {
                objectDb.ExecuteNonQuery(criteria.GetSpParams());
                var outputs = objectDb.GetOuputParams();
                custId = Converter.ToInt(outputs["CustId"]);
            }

            return custId;
        }
    }
}
