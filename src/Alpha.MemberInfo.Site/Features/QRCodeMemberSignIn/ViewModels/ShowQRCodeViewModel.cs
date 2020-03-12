using Alpha.Web.Helpers;

namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.ViewModels
{
    public class ShowQRCodeViewModel
    {
        public ShowQRCodeViewModel(
            decimal credit,
            string username,
            int custId,
            string firstName,
            string qrCode, 
            string qrCodeLink)
        {
            Credit = credit;
            Username = username;
            CustId = custId;
            FirstName = firstName;
            QRCode = qrCode;
            QRCodeLink = qrCodeLink;
        }

        public decimal Credit { get; }

        public string Username { get; }

        public int CustId { get; }

        public string FirstName { get; }

        public string QRCode { get; }

        public string QRCodeLink { get; }

        public string UpdateInformationUrl 
            => $"{MicroSites.OldMainUrl()}_AccountInfo/EditCustomerInfo.aspx?Username={Username}&CustId={CustId}";

        public string UpdateCreditUrl => string.Empty;
    }
}