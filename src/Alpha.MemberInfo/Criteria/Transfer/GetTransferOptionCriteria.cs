using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.Data.Repository;

namespace Alpha.MemberInfo.Criteria.Transfer
{
    public class GetTransferOptionCriteria : NonQueryCommand
    {
        public GetTransferOptionCriteria(int custId)
        {
            CustId = custId;
            TransferOption = string.Empty;
        }

        public int CustId { get; }

        public string TransferOption { get; set; }

        public override string GetSettingKey() => "Acc_CustomerSelTransferOption";

        public override bool IsValid() => CustId > 0;
    }
}
