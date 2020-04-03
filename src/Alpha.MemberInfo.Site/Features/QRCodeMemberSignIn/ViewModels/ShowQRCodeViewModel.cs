namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.ViewModels
{
    using Alpha.Web.Helpers;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class ShowQRCodeViewModel
    {
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
            bool showPageTitle)
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
            Domains = domains.Select((item, index) =>
            {
                return new SelectListItem
                {
                    Value = item,
                    Text = index.Equals(0) ? "Main Domain" : item
                };
            });

            ShowMessage = showMessage;
            ShowPageTitle = showPageTitle;
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

        public IEnumerable<SelectListItem> Domains { get; }

        public bool ShowMessage { get; }

        public bool ShowPageTitle { get; }

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