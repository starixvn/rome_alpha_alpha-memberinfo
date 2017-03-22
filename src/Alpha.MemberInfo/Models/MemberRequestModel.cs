namespace Alpha.MemberInfo.Models
{
    public class MemberRequestModel
    {
        public MemberRequestModel(
            int custId,
            string username,
            bool sync,
            bool enable,
            decimal balance,
            decimal outstanding,
            decimal availableCredit)
        {
            CustId = custId;
            Username = username;
            Sync = sync;
            Enable = enable;
            Balance = balance;
            Outstanding = outstanding;
            AvailableCredit = availableCredit;
        }
        
        public int CustId { get; }

        public string Username { get; }

        public bool Sync { get; }

        public bool Enable { get; }

        public decimal Balance { get; }

        public decimal Outstanding { get; }

        public decimal AvailableCredit { get; }
    }
}