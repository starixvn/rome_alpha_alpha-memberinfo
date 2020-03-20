namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.ViewModels
{
    using Alpha.Web.Helpers;

    public class ShowQRCodeViewModel
    {
        public ShowQRCodeViewModel(
            decimal credit,
            decimal creditMin,
            decimal creditMax,
            string searchAccountName,
            int searchAccountId,
            string firstName,
            string qrCode,
            string qrCodeLink,
            bool showMessage)
        {
            Credit = credit;
            CreditMin = creditMin;
            CreditMax = creditMax;
            SearchAccountName = searchAccountName;
            SearchAccountId = searchAccountId;
            FirstName = firstName;
            QRCode = qrCode;
            QRCodeLink = qrCodeLink;
            ShowMessage = showMessage;
        }

        public decimal Credit { get; }

        public decimal CreditMin { get; }

        public decimal CreditMax { get; }

        public string SearchAccountName { get; }

        public int SearchAccountId { get; }

        public string FirstName { get; }

        public string QRCode { get; }

        public string QRCodeLink { get; }

        public bool ShowMessage { get; }

        // TODO: hardcode syncCasino
        public string UpdateInformationUrl
             => $"{MicroSites.OldMainUrl()}_AccountInfo/EditCustomerInfo.aspx?" +
             $"Username={SearchAccountName}&CustId={SearchAccountId}&isSyncCasino=1";

        public string UpdateCreditUrl
            => $"{MicroSites.AccountInfoUrl()}SearchAccount/GivenCredit" +
            $"?SearchAccountId={SearchAccountId}&SearchAccountName={SearchAccountName}&SearchAccountRoleId=1" +
            $"&CurrentGivenCredit={Credit}&GivenCreditMin={CreditMin}&GivenCreditMax={CreditMax}";
    }
}