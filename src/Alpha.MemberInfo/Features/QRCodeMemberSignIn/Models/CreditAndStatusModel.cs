namespace Alpha.MemberInfo.Features.QRCodeMemberSignIn.Models
{
    using System;
    using Alpha.MemberInfo.AppResources;

    public class CreditAndStatusModel
    {
        public CreditAndStatusModel()
        {
        }

        public CreditAndStatusModel(
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

        public decimal Credit { get; set; }

        public decimal CreditMin { get; set; }

        public decimal CreditMax { get; set; }

        public decimal MemberMaxCredit { get; set; }

        public string Status { get; set; }

        public string GivenCreditMustBeBetweenMinMaxMessage
            => string.Format(
                Resources.GivenCreditMustBeBetweenMinMax,
                string.Format("{0:#,0.##}", CreditMin),
                string.Format("{0:#,0.##}", MemberMaxCredit > 0 ? Math.Min(MemberMaxCredit, CreditMax) : CreditMax));
    }
}