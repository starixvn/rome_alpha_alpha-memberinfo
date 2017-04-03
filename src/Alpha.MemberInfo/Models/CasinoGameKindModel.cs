namespace Alpha.MemberInfo.Models
{
    public class CasinoGameKindModel
    {
        public CasinoGameKindModel(
            int id,
            decimal positionTaking,
            decimal commission)
        {
            Id = id;
            PositionTaking = positionTaking;
            Commission = commission;
        }

        public int Id { get; }

        public decimal PositionTaking { get; }

        public decimal Commission { get; }
    }
}
