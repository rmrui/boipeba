using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Boipeba.Core.Auth;
using Boipeba.Core.Auth.Exceptions;
using Boipeba.Core.Auth.Services;
using Boipeba.Core.Domain.Controller;

namespace Boipeba.Controllers
{
    public class HomeController : Controller, IBoipebaController
    {
        private readonly IAuthenticationService _authService;

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
        [ValidateAntiForgeryToken]
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

        private bool IsMobileBrowser()
        {
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

    public class LoginVM
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}