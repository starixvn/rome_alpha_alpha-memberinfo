namespace Alpha.MemberInfo.Models.CreditBalance
{
    using System;
    using AppResources;

    public class CreditBalanceItem
    {
        public CreditBalanceItem()
        {
        }

        public CreditBalanceItem(
            int rowNumber,
            bool enabled,
            bool closed,
            bool suspended,
            int custId,
            string userName,
            string firstName,
            string lastName,
            decimal credit,
            decimal betCredit,
            decimal balance,
            decimal yesterdayBalance,
            decimal outstanding,
            DateTime lastLogged,
            string ipLogin)
        {
            RowNumber = rowNumber;
            Enabled = enabled;
            Closed = closed;
            Suspended = suspended;
            CustId = custId;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Credit = credit;
            BetCredit = betCredit;
            Balance = balance;
            Yesterday_Balance = yesterdayBalance;
            Outstanding = outstanding;
            LastLogged = lastLogged;
            IPLogin = ipLogin;
        }

        public int RowNumber { get; }

        public bool Enabled { get; }

        public bool Closed { get; }

        public byte ClosedReason { get; set; }

        public bool Suspended { get; }

        public int CustId { get; }

        public string UserName { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public decimal Credit { get; }

        public decimal BetCredit { get; set; }

        public decimal Balance { get; }

        public decimal Yesterday_Balance { get; }

        public decimal Outstanding { get; }

        public DateTime? LastLogged { get; }

        public string IPLogin { get; set; }

        public string Status
        {
            get
            {
                if (!Enabled)
                {
                    return Resources.Disabled;
                }

                if (Closed)
                {
                    return Resources.Closed;
                }

                if (Suspended)
                {
                    return Resources.Suspended;
                }

                return Resources.Open;
            }
        }
    }
}
