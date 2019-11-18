using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Token.DataObject.Request;
using Token.DataObject.Response;
using Token.Infrastructure.Context;
using Token.Service.Business;
using Token.Service.Proxy.Configuration;

namespace Token.Service.Proxy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        protected UserService UserService { get; set; }

        public TokenController(UserContext context)
        {
            this.UserService = new UserService(context);
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "up";
        }

        [Authorize("Bearer")]
        [HttpGet("validate")]
        public ActionResult<bool> ValidateToken()
        {
            return true;
        }

        [HttpPost("generate")]
        public ActionResult<TokenResponseDto> Post([FromBody] DataObject.Request.UserDto userRequest, [FromServices] SigningConfigurations signing, [FromServices]TokenConfigurations tokenConfig)
        {
            try
            {
                var user = this.UserService.GetUser(userRequest.Username, userRequest.Password);
                

                if (user != null)
                {
                    ClaimsIdentity identity = new ClaimsIdentity(
                        new GenericIdentity(user.AccessKey, "AccesKey"),
                        new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                        }
                    );

                    DateTime dataCriacao = DateTime.Now;
                    DateTime dataExpiracao = dataCriacao + TimeSpan.FromSeconds(tokenConfig.Seconds);

                    var handler = new JwtSecurityTokenHandler();
                    var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                    {
                        Issuer = tokenConfig.Issuer,
                        Audience = tokenConfig.Audience,
                        SigningCredentials = signing.SigningCredentials,
                        Subject = identity,
                        NotBefore = dataCriacao,
                        Expires = dataExpiracao
                    });
                    var token = handler.WriteToken(securityToken);

                    
                    return this.SetResponse(token, true, dataCriacao, dataExpiracao, "Authenticated");
                }
                else
                {
                    return this.SetResponseFail("Authentication Fail!");
                }
            }
            catch(Exception ex)
            {
                return this.SetResponseFail($"Authentication Fail! - Exception: {ex.StackTrace}");
            }
        }

        private TokenResponseDto SetResponse(string token, bool authenticated, DateTime? createDate, DateTime? expirationDate, string message)
        {
            TokenResponseDto response = new TokenResponseDto
            {
                AccessToken = token,
                Authenticated = authenticated,
                CreateDate = createDate,
                ExpirationDate = expirationDate,
                Message = message
            };

            return response;
        }

        private TokenResponseDto SetResponseFail(string message)
        {
            return this.SetResponse(null, false, null, null, message);
        }
    }
}
