namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.ViewModels
{
    using Alpha.Web.Helpers;

    public class ShowQRCodeViewModel
    {
        private readonly bool syncRnGCasino;
        private readonly int custIdFrom;

        public ShowQRCodeViewModel(
            decimal credit,
            decimal creditMin,
            decimal creditMax,
            string searchAccountName,
            int searchAccountId,
            string firstName,
            string qrCode,
            string qrCodeLink,
            string[] domains,
            bool createMode,
            bool showPageTitle,
            bool syncRnGCasino,
            int custIdFrom,
            string rawPassword)
        {
            Credit = credit;
            CreditMin = creditMin;
            CreditMax = creditMax;
            SearchAccountName = searchAccountName;
            SearchAccountId = searchAccountId;
            FirstName = firstName;
            QRCode = qrCode;
            QRCodeLink = qrCodeLink;
            Domains = domains;
            CreateMode = createMode;
            ShowPageTitle = showPageTitle;
            RawPassword = rawPassword;
            this.syncRnGCasino = syncRnGCasino;
            this.custIdFrom = custIdFrom;
        }

        public string RawPassword { get; }

        public decimal Credit { get; }

        public decimal CreditMin { get; }

        public decimal CreditMax { get; }

        public string SearchAccountName { get; }

        public int SearchAccountId { get; }

        public string FirstName { get; }

        public string QRCode { get; }

        public string QRCodeLink { get; }

        public string[] Domains { get; }

        public bool CreateMode { get; }

        public bool ShowPageTitle { get; }

        public string UpdateInformationUrl
             => $"{MicroSites.OldMainUrl()}_AccountInfo/EditCustomerInfo.aspx?" +
             $"Username={SearchAccountName}&CustId={SearchAccountId}&isSyncCasino={(syncRnGCasino ? "1":"0")}";

        public string UpdateCreditUrl
            => $"{MicroSites.AccountInfoUrl()}SearchAccount/GivenCredit" +
            $"?SearchAccountId={SearchAccountId}&SearchAccountName={SearchAccountName}&SearchAccountRoleId=1" +
            $"&GivenCreditMin={CreditMin}&GivenCreditMax={CreditMax}";

        public string CorrectDataUrl
            => $"{MicroSites.OldMainUrl()}/QRCodeMemberSignIn/CorrectData?custId={SearchAccountId}&custIdFrom={custIdFrom}&username={SearchAccountName}";
    }
}