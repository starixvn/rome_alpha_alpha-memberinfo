namespace Alpha.MemberInfo.Features.QRCodeMemberSignIn.Models
{
    public class ResultCreateMemberQRCode
    {
        public int CustId { get; set; }

        public bool Success { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }
    }
}