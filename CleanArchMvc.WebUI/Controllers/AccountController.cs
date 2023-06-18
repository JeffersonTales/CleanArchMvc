using CleanArchMvc.Domain.Account;
using CleanArchMvc.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchMvc.WebUI.Controllers {
    public class AccountController : Controller {

        #region Atributos
        private readonly IAuthenticate _authenticate;
        #endregion

        #region Construtor
        public AccountController(IAuthenticate authenticate) {
            this._authenticate = authenticate;
        }
        #endregion

        [HttpGet]
        public IActionResult Login(string returnUrl) {

            //try {
            //    return Redirect(returnUrl);
            //}
            //catch (System.Exception) {

            //    return View(new LoginViewModel() { ReturnUrl = returnUrl });
            //}

            return View(new LoginViewModel() { ReturnUrl = returnUrl });

        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model) {

            var result = await this._authenticate.Authenticate(email: model.Email,
                                                               password: model.Password);

            if (result) {
                if (string.IsNullOrEmpty(model.ReturnUrl)) return RedirectToAction(actionName: "Index", controllerName: "Home");

                return Redirect(model.ReturnUrl);
            }
            else {
                this.ModelState.AddModelError(string.Empty, "Invalid login attempt (password must be strong).");
                return View(model);
            }

        }

        [HttpGet]
        public IActionResult Register() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model) {

            var result = await this._authenticate.RegisterUser(email: model.Email,
                                                               password: model.Password);

            if (result) {
                return Redirect("/");//Home index
            }
            else {
                this.ModelState.AddModelError(string.Empty, "Invalid register attempt (password must be strong).");
                return View(model);
            }

        }

        public async Task<IActionResult> Logout() {
            await this._authenticate.Logout();
            return Redirect("/Account/Login");
        }

    }
}
