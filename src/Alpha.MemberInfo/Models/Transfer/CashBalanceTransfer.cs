using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.MemberInfo.Models.Transfer
{
    public class CashBalanceTransfer
    {
        public int CustId { get; set; }

        public string UserName { get; set; }

        public byte RoleId { get; set; }

        public decimal UplineCashBalance { get; set; }

        public decimal CashBalance { get; }

        public decimal Outstanding { get; }

        public decimal GivenCredit { get; }
    }
}
