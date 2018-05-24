using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Security;
using Boipeba.Core.Auth;
using Boipeba.Core.Auth.Exceptions;
using Boipeba.Core.Auth.Services;
using Boipeba.Core.Domain.Controller;
using Boipeba.Web.Models;

namespace Boipeba.Web.Controllers
{
    public class HomeController : Controller, IBoipebaController
    {
        private readonly  IAuthenticationService _authService;

        public HomeController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        public ActionResult SignIn()
        {
            if (User is Usuario)
                return StartPage();

            ViewBag.Version = GetVersion();

            return View();
        }


        /// <summary>
        /// Ação de login no portal.
        /// </summary>
        /// <returns>view</returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public virtual ActionResult SignIn(LoginVM model)
        {
            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                return View(model);
            }

#if !DEBUG
            var validCaptcha = IsReCaptchaValid();
            var fallbackCaptcha = this.IsCaptchaValid(string.Empty);

            if (!(validCaptcha || fallbackCaptcha))
            {
                var msg = IsFallBackCaptcha()
                    ? "CAPTCHA não preenchido corretamente. Digite os caracteres impressos na imagem."
                    : "CAPTCHA não respondido. Por favor, responda o captcha de segurança.";

                ModelState.AddModelError("Captcha", msg);
                return View(model);
            }
#endif

            try
            {
                var userdata = _authService.SignIn(model.Username, model.Password, model.RememberMe, false, IsMobileBrowser());

                return StartPage();
            }
            catch (InvalidLoginException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            catch (InvalidAccessException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            catch (AccessExpirationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            catch (InvalidMatriculaException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

                ModelState.AddModelError(string.Empty, "Serviço Indisponível");
            }

            ViewBag.Version = GetVersion();

            return View("SignIn", model);
        }

        //regex from http://detectmobilebrowsers.com/
        private static readonly Regex b = new Regex(@"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        private static readonly Regex v = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase | RegexOptions.Multiline);

        public bool IsMobileBrowser()
        {
            var userAgent = Request.Headers["User-Agent"];

            if ((b.IsMatch(userAgent) || v.IsMatch(userAgent.Substring(0, 4))))
            {
                return true;
            }

            return false;
        }


        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("SignIn", "Home");
        }

        private dynamic GetVersion()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;

#if !DEBUG
            var name = "build";
#else
            var name = version.Build == 0
                ? "debug"
                : "teste";
#endif

            return $"v{version.Major}.{version.Minor} {name} {version.Build}";
        }

        private RedirectToRouteResult StartPage()
        {
            return RedirectToAction("Index", "Dashboard");
        }
    }
}