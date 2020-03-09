namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.ViewModels
{
    public class MemberCreditRange
    {
        public MemberCreditRange(
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