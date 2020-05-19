namespace Alpha.MemberInfo.DataAccesses
{
    using System;
    using System.Collections.Generic;
    using Criteria.WinLossLimit;
    using Fanex.Common;
    using Models.WinLossLimit;
    using System.Linq;
    using System.Data;
    using Fanex.Data.Repository;

    public interface WinLossLimitDataAccess
    {
        WinLossLimitPage GetWinLossLimits(GetCustWinLossLimitCriteria criteria);

        IEnumerable<int> MapPlayerIdToCustId(MapPlayerId3rdToCustIdCriteria criteria);
    }

    public class WinLossLimitDataAccessImpl : WinLossLimitDataAccess
    {
        private readonly IDynamicRepository repository;

        public WinLossLimitDataAccessImpl() : this(null) { }

        internal WinLossLimitDataAccessImpl(IDynamicRepository repository)
        {
            this.repository = repository ?? new DynamicRepository();
        }

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
                                PlayerId = GetFieldValue(reader, "PlayerId"),
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

        public IEnumerable<int> MapPlayerIdToCustId(MapPlayerId3rdToCustIdCriteria criteria)
        {
            return repository.Fetch<int>(criteria);
        }

        private static string GetFieldValue(IDataReader reader, string fieldName)
            => HasColumn(reader, fieldName) && !IsNullOrDBNullValue(reader, fieldName) ? (string)reader[fieldName] : "-";

        private static bool HasColumn(IDataRecord dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsNullOrDBNullValue(IDataReader reader, string fieldName)
            => reader[fieldName] == null || reader[fieldName] == DBNull.Value;
    }
}
