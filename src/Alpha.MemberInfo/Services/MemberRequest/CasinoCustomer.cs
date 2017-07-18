using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Alpha.MemberInfo.Models;
using Alpha.MemberInfo.Models.MemberRequests;
using Alpha.MemberInfo.Params;
using CasinoLibrary.SBAgent;
using Fanex.Common;

namespace Alpha.MemberInfo.Services.MemberRequest
{
    public interface CasinoCustomer
    {
        GetCasinoSettingResult GetSetting(ProductSettingParam productSettingParam);
    }

    public class CasinoCustomerImpl : CasinoCustomer
    {
        public GetCasinoSettingResult GetSetting(ProductSettingParam productSettingParam)
        {
            GetCasinoSettingResult result = null;

            try
            {
                var gameKinds = GameKinds(productSettingParam.CustId);
                result = new GetCasinoSettingResult(gameKinds, 0, string.Empty);
            }
            catch (Exception ex)
            {
                result = new GetCasinoSettingResult(Enumerable.Empty<CasinoGameKindModel>(), 1, ex.Message);
            }

            return result;
        }

        public virtual IEnumerable<CasinoGameKindModel> GameKinds(int custId)
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
