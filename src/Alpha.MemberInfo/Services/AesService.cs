using Fanex.Cryptography.Aes;

namespace Alpha.MemberInfo.Services
{
    public interface AesService
    {
        string Decrypt(string cipherText, string secretKey);
    }

    public class AesServiceImpl : AesService
    {
        public string Decrypt(string cipherText, string secretKey)
        {
            try
            {
                return AesExtension.Decrypt(cipherText, secretKey);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
