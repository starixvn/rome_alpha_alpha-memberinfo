using System.Collections.Generic;
using System.Data;
using Alpha.MemberInfo.Models;
using CasinoLibrary.SBAgent;
using Fanex.Common;

namespace Alpha.MemberInfo.DataAccesses
{
    public interface CasinoDataAccess
    {
        IEnumerable<CasinoGameKindModel> GameKinds(int custId);
    }

    public class CasinoDataAccessImpl : CasinoDataAccess
    {
        public IEnumerable<CasinoGameKindModel> GameKinds(int custId)
        {
            var cust = new Customer();
            var gameKinds = cust.GetCustGameKind(custId);
            var result = new List<CasinoGameKindModel>();

            foreach (DataRow row in gameKinds.Rows)
            {
                result.Add(new CasinoGameKindModel(
                    Converter.ToInt(row["GameKind"]),
                    Converter.ToDecimal(row["APositionTaking"]),
                    Converter.ToDecimal(row["MemberRollingRate"])));
            }

            return result;
        }
    }
}
