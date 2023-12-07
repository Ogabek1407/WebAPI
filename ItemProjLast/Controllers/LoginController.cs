using ItemProjLast.Domian.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ItemProjLast.Controllers
{
    [Controller]
    public class LoginController:ControllerBase
    {
        public LoginController(IOptions<AppSettings> appSettings)
        {
            AppSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
            securityTokenHandler = new JwtSecurityTokenHandler();
        }
        
        public IOptions<AppSettings> AppSettings { get; }

        private JwtSecurityTokenHandler securityTokenHandler;
        
        [AllowAnonymous]
        [HttpPost]
        [Route("Token")]
        public async ValueTask<ActionResult<string>> Token([FromBody]LoginDto loginDto)
        {
            if(loginDto == null)
            {
                return BadRequest("not fount");
            }
            string Token="";
            if (loginDto.Password==302 && loginDto.Login == "Space")
            {
                var key = Encoding.ASCII.GetBytes(AppSettings.Value.SecretKey);
                var TokenDescriptior = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(
                        new Claim[]
                        {
                            new Claim(ClaimTypes.GivenName,"Space"),
                            new Claim(ClaimTypes.Name,"302"),
                            new Claim(ClaimTypes.Role,"Elbek7")
                        }),
                    Expires = DateTime.UtcNow.AddDays(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                };
                SecurityToken securityToken = securityTokenHandler.CreateToken(TokenDescriptior);
                Token=securityTokenHandler.WriteToken(securityToken);
            }
            if (string.IsNullOrEmpty(Token))
            {
                return BadRequest("Error");
            }
            return Ok(Token);
            }
        }
}
