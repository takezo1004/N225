using N225.Domain.Modules.Utils;
using N225.Infrastrucure.KubuAPIs;

namespace N225.Infrastrucure.KabuSuit
{
    public class KabuSuiteApiToken
    {
        /// <summary>
        /// 認証 APIトークンを取得
        /// </summary>
        /// <param name="apiPassword"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public static string Token(string apiPassword, string port)
        {
            Validate.ValidateToken(apiPassword, port);
            return GenerateToken.GetToken(apiPassword);

        }
    }
}
