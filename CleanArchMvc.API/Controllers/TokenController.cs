using CleanArchMvc.API.Models;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase {

        #region Atributos
        private readonly IAuthenticate _authentication;
        private readonly IConfiguration _configuration;
        #endregion

        #region Construtor
        public TokenController(IAuthenticate authentication,
                               IConfiguration configuration) {

            this._authentication = authentication ?? throw new ArgumentNullException(nameof(authentication));
            this._configuration = configuration;
        }
        #endregion

        [HttpPost("CreateUser")]
        [ApiExplorerSettings(IgnoreApi =true)]
        [Authorize]
        public async Task<ActionResult> CreateUser([FromBody] LoginModel userInfo) {

            var result = await this._authentication.RegisterUser(userInfo.Email, userInfo.Password);

            if (result) {
                
                return this.Ok($"User {userInfo.Email} was create successfully");
            }
            else {
                this.ModelState.AddModelError(string.Empty, "Invalid User attempt.");
                return BadRequest(this.ModelState);
            }

        }

        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel userInfo) {

            var result = await this._authentication.Authenticate(userInfo.Email,
                                                                 userInfo.Password);

            if (result) {
                return GenerateToken(userInfo);
                //return this.Ok($"{userInfo.Email} login successfully");
            }
            else {
                this.ModelState.AddModelError(string.Empty, "Invalid Login attempt.");
                return BadRequest(this.ModelState);
            }

        }

        private UserToken GenerateToken(LoginModel userInfo) {

            #region Declarações do Usuário
            var claims = new[] {
                new Claim("email", userInfo.Email),
                new Claim("meuvalor","oque voce quiser"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            #endregion

            #region Geração da chave privada para assinar o token
            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["Jwt:SecretKey"]));
            #endregion

            #region Geração da assinatura digital
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);
            #endregion

            #region Definir o tempo de expiração
            var expiration = DateTime.UtcNow.AddMinutes(10);
            #endregion

            #region Gerar o Token
            JwtSecurityToken token = new JwtSecurityToken(issuer: this._configuration["Jwt:Issuer"], //emissor
                                                          audience: this._configuration["Jwt:Audience"], //audiencia
                                                          claims: claims, //claims
                                                          expires: expiration, //data de expiracao
                                                          signingCredentials: credentials //assinatura digital
                                                          );
            #endregion

            return new UserToken() {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };

        }
    }
}
