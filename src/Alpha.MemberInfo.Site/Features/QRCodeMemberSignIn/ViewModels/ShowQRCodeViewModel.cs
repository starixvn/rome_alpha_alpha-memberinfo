namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.ViewModels
{
    public class ShowQRCodeViewModel
    {
        public ShowQRCodeViewModel(
            decimal credit,
            string username,
            string firstName,
            string qrCode, 
            string qrCodeLink)
        {
            Credit = credit;
            Username = username;
            FirstName = firstName;
            QRCode = qrCode;
            QRCodeLink = qrCodeLink;
        }

        public decimal Credit { get; }

        public string Username { get; }

        public string FirstName { get; }

        public string QRCode { get; }

        public string QRCodeLink { get; }

        public string UpdateInformationUrl => string.Empty;

        public string UpdateCreditUrl => string.Empty;
    }
}