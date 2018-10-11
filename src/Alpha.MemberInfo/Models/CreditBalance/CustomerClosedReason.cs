namespace Alpha.MemberInfo.Models.CreditBalance
{
    public class CustomerClosedReason
    {
        public CustomerClosedReason()
        {
        }

        public CustomerClosedReason(int custId, byte closedReason)
        {
            CustId = custId;
            ClosedReason = closedReason;
        }

        public int CustId { get; }

        public byte ClosedReason { get; }
    }
}
