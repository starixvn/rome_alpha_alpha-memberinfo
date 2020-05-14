namespace Alpha.MemberInfo.Features.QRCodeMemberSignIn.Models
{
    using Alpha.MemberInfo.AppResources;

    public class CreditRangeModel
    {
        public CreditRangeModel()
        {
        }

        public CreditRangeModel(
            decimal credit,
            decimal creditMin,
            decimal creditMax,
            string status)
        {
            Credit = credit;
            CreditMin = creditMin;
            CreditMax = creditMax;
            Status = status;
        }

        public decimal Credit { get; }

        public decimal CreditMin { get; }

        public decimal CreditMax { get; }

        public string Status { get; }

        public string GivenCreditMustBeBetweenMinMaxMessage
            => string.Format(
                Resources.GivenCreditMustBeBetweenMinMax,
                string.Format("{0:#,0.##}", CreditMin),
                string.Format("{0:#,0.##}", CreditMax));
    }
}
