namespace Alpha.MemberInfo.Models.CreditBalance
{
    public class AgentNetPosition
    {
        public AgentNetPosition()
        {
        }

        public AgentNetPosition(int custId, decimal netPosition)
        {
            CustId = custId;
            NetPosition = netPosition;
        }

        public int CustId { get; }

        public decimal NetPosition { get; }
    }
}
