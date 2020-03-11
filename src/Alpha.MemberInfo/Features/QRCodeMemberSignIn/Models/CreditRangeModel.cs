namespace Alpha.MemberInfo.Features.QRCodeMemberSignIn.Models
{
    public class CreditRangeModel
    {
        public CreditRangeModel()
        {
        }

        public CreditRangeModel(
            decimal credit,
            decimal creditMin,
            decimal creditMax)
        {
            Credit = credit;
            CreditMin = creditMin;
            CreditMax = creditMax;
        }

        public decimal Credit { get; }

        public decimal CreditMin { get; }

        public decimal CreditMax { get; }
    }
}
