namespace Alpha.MemberInfo.Models.MemberRequests
{
    public class MemberRequestModel
    {
        public MemberRequestModel()
        {
        }

        public MemberRequestModel(
            int custId,
            string username,
            bool sync,
            decimal balance,
            decimal outstanding,
            decimal availableCredit)
        {
            CustId = custId;
            Username = username;
            Sync = sync;
            Balance = balance;
            Outstanding = outstanding;
            AvailableCredit = availableCredit;
        }
        
        public int CustId { get; set; }

        public string Username { get; set; }

        public bool Sync { get; }

        public decimal Balance { get; }

        public decimal Outstanding { get; }

        public decimal AvailableCredit { get; }
    }
}