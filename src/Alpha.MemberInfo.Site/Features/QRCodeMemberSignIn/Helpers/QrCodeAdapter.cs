namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Helpers
{
    public interface QrCodeAdapter
    {
        string Generate(string url);
    }
}