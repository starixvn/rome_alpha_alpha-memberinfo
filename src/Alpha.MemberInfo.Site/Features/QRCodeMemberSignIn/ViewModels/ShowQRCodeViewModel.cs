namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.ViewModels
{
    using Alpha.Web.Helpers;

    public class ShowQRCodeViewModel
    {
        private bool syncRnGCasino;

        public ShowQRCodeViewModel(
            decimal credit,
            decimal creditMin,
            decimal creditMax,
            string memberNameCopyFrom,
            string searchAccountName,
            int searchAccountId,
            string firstName,
            string qrCode,
            string qrCodeLink,
            string[] domains,
            bool showMessage,
            bool showPageTitle,
            bool syncRnGCasino)
        {
            Credit = credit;
            CreditMin = creditMin;
            CreditMax = creditMax;
            MemberNameCopyFrom = memberNameCopyFrom;
            SearchAccountName = searchAccountName;
            SearchAccountId = searchAccountId;
            FirstName = firstName;
            QRCode = qrCode;
            QRCodeLink = qrCodeLink;
            Domains = domains;
            ShowMessage = showMessage;
            ShowPageTitle = showPageTitle;
            this.syncRnGCasino = syncRnGCasino;
        }

        public decimal Credit { get; }

        public decimal CreditMin { get; }

        public decimal CreditMax { get; }

        public string MemberNameCopyFrom { get; }

        public string SearchAccountName { get; }

        public int SearchAccountId { get; }

        public string FirstName { get; }

        public string QRCode { get; }

        public string QRCodeLink { get; }

        public string[] Domains { get; }

        public bool ShowMessage { get; }

        public bool ShowPageTitle { get; }

        // TODO: hardcode syncCasino
        public string UpdateInformationUrl
             => $"{MicroSites.OldMainUrl()}_AccountInfo/EditCustomerInfo.aspx?" +
             $"Username={SearchAccountName}&CustId={SearchAccountId}&isSyncCasino={syncRnGCasino}";

        public string UpdateCreditUrl
            => $"{MicroSites.AccountInfoUrl()}SearchAccount/GivenCredit" +
            $"?SearchAccountId={SearchAccountId}&SearchAccountName={SearchAccountName}&SearchAccountRoleId=1" +
            $"&CurrentGivenCredit={Credit}&GivenCreditMin={CreditMin}&GivenCreditMax={CreditMax}";
    }
}