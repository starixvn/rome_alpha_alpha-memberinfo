namespace Alpha.MemberInfo.Models
{
    public class CustomerInfo
    {
        public CustomerInfo()
        {
        }

        public CustomerInfo(string username, int custId)
        {
            Username = username;
            CustId = custId;
        }

        public string Username { get; }

        public int CustId { get; }
    }
}
