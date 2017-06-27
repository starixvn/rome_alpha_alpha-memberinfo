using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.MemberInfo.Models.Transfer
{
    public class CashBalanceTransfer
    {
        public CashBalanceTransfer()
        {
        }

        public CashBalanceTransfer(
            int custId,
            string userName,
            byte roleId,
            decimal uplineCashBalance,
            decimal cashBalance,
            decimal outstanding,
            decimal givenCredit)
        {
            CustId = custId;
            UserName = userName;
            RoleId = roleId;
            UplineCashBalance = uplineCashBalance;
            CashBalance = cashBalance;
            Outstanding = outstanding;
            GivenCredit = givenCredit;
        }

        public int CustId { get; }

        public string UserName { get; }

        public byte RoleId { get; }

        public decimal UplineCashBalance { get; }

        public decimal CashBalance { get; }

        public decimal Outstanding { get; }

        public decimal GivenCredit { get; }
    }
}
