using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Infra.Data.Identity {

    public class AuthenticateService : IAuthenticate {

        #region Atributos
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        #endregion

        #region Construtor
        public AuthenticateService(SignInManager<ApplicationUser> signInManager, 
                                   UserManager<ApplicationUser> userManager) {

            this._userManager = userManager;
            this._signInManager = signInManager;
        }
        #endregion

        #region Métodos do Contrato
        public async Task<bool> Authenticate(string email, string password) {
            var result = await this._signInManager.PasswordSignInAsync(userName: email, 
                                                                       password: password,
                                                                       isPersistent: false, 
                                                                       lockoutOnFailure: false);

            return result.Succeeded;

        }

        public async Task<bool> RegisterUser(string email, string password) {
            
            var applicationUser = new ApplicationUser { UserName = email, 
                                                        Email = email };

            var result = await this._userManager.CreateAsync(user: applicationUser, 
                                                             password: password);

            if (result.Succeeded) {
                await this._signInManager.SignInAsync(user: applicationUser, 
                                                      isPersistent: false);
            }

            return result.Succeeded;

        }

        public async Task Logout() {
            await this._signInManager.SignOutAsync();
        }
        #endregion

    }
}
