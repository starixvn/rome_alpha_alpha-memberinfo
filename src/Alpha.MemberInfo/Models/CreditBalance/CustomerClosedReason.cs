namespace Alpha.MemberInfo.Models.CreditBalance
{
    public class CustomerClosedReason
    {
        public CustomerClosedReason()
        {
        }

        public CustomerClosedReason(int custId, byte closedReason, byte locked)
        {
            CustId = custId;
            ClosedReason = closedReason;
            Locked = locked;
        }

        public int CustId { get; }

        public byte ClosedReason { get; }

        public byte Locked { get; }
    }
}
