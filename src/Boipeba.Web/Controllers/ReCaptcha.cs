#pragma warning disable 1591

using System.Web.Script.Serialization;
using Boipeba.Core.Domain.Services;

namespace Boipeba.Web.Controllers
{
    public interface IReCaptcha: IService
    {
        string Validate(string EncodedResponse, string ip);
    }

    public class ReCaptchaClass: IReCaptcha
    {
        public string Validate(string EncodedResponse, string ip)
        {
            var client = new System.Net.WebClient();

            string PrivateKey = "6LcJ5ycTAAAAAAqtxTry74BuiintLPec22UOV7TM";

            var GoogleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}&remoteip={2}", PrivateKey, EncodedResponse, ip));

            var captchaResponse = new JavaScriptSerializer().Deserialize<ReCaptchaClass>(GoogleReply);

            return captchaResponse.success;
        }

        public string success { get; set; }
    }
}
