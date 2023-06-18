using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Infra.Data.Identity {

    public class SeedUserRoleInitial : ISeedUserRoleInitial {

        #region Atributos
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        #endregion

        #region
        public SeedUserRoleInitial(RoleManager<IdentityRole> roleManager,
                                   UserManager<ApplicationUser> userManager) {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }
        #endregion

        #region Métodos do Contrato
        public void SeedRoles() {

            if (this._userManager.FindByEmailAsync("usuario@localhost").Result == null) {

                ApplicationUser user = new ApplicationUser() {
                    UserName = "usuario@localhost",
                    Email = "usuario@localhost",
                    NormalizedUserName = "USUARIO@LOCALHOST",
                    NormalizedEmail = "USUARIO@LOCALHOST",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                IdentityResult result = this._userManager.CreateAsync(user: user,
                                                                      password: "Numsey#2021").Result;

                if (result.Succeeded) {

                    this._userManager.AddToRoleAsync(user,
                                                     "User").Wait();
                }

            }

            if (this._userManager.FindByEmailAsync("admin@localhost").Result == null) {

                ApplicationUser user = new ApplicationUser() {
                    UserName = "admin@localhost",
                    Email = "admin@localhost",
                    NormalizedUserName = "ADMIN@LOCALHOST",
                    NormalizedEmail = "ADMIN@LOCALHOST",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                IdentityResult result = this._userManager.CreateAsync(user: user,
                                                                      password: "Numsey#2021").Result;

                if (result.Succeeded) {

                    this._userManager.AddToRoleAsync(user,
                                                     "Admin").Wait();
                }

            }

        }

        public void SeedUsers() {

            if (!this._roleManager.RoleExistsAsync("User").Result) {

                IdentityRole role = new IdentityRole() {
                    Name = "User",
                    NormalizedName = "USER"
                };

                IdentityResult roleResult = this._roleManager.CreateAsync(role).Result;
            }

            if (!this._roleManager.RoleExistsAsync("Admin").Result) {

                IdentityRole role = new IdentityRole() {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                };

                IdentityResult roleResult = this._roleManager.CreateAsync(role).Result;
            }

        }
        #endregion
    }
}
