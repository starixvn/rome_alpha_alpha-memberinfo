namespace Alpha.MemberInfo.Models
{
    public class MemberRequestModel
    {
        public MemberRequestModel(
            string username,
            bool enable,
            decimal balance,
            decimal outstanding,
            decimal availableCredit)
        {
            Username = username;
            Enable = enable;
            Balance = balance;
            Outstanding = outstanding;
            AvailableCredit = availableCredit;
        }
        
        public string Username { get; }

        public bool Enable { get; }

        public decimal Balance { get; }

        public decimal Outstanding { get; }

        public decimal AvailableCredit { get; }
    }
}