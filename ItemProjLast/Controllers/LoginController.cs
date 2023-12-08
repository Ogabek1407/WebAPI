using Interface;
using ItemProjLast.Domian.Dto;
using ItemProjLast.Domian.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public LoginController(IOptions<AppSettings> appSettings,IUserRepository userRepository)
        {
            AppSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
            UserRepository = userRepository;
            securityTokenHandler = new JwtSecurityTokenHandler();
        }
        
        public IOptions<AppSettings> AppSettings { get; }
        public IUserRepository UserRepository { get; }

        private JwtSecurityTokenHandler securityTokenHandler;
        
        [AllowAnonymous]
        [HttpPost]
        [Route("Token")]
        public async ValueTask<ActionResult<string>> Token([FromBody]LoginDto loginDto)
        {
            if(loginDto == null)
            {
                return BadRequest("argument null");
            }
            string Token="";
            var user = await UserRepository.
                GetAll().
                Where(x =>
                x.Email == loginDto.Email &&
                x.Password == loginDto.Password).FirstOrDefaultAsync();
          
            if (user is not null)
            {
                var key = Encoding.ASCII.GetBytes(AppSettings.Value.SecretKey);
                var TokenDescriptior = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(
                        new Claim[]
                        {
                            new(ClaimTypes.GivenName,user.FirstName),
                            new(ClaimTypes.Name,user.LastName),
                            new(ClaimTypes.SerialNumber,user.Email.ToString())
                        }),
                    Expires = DateTime.UtcNow.AddDays(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                };
                SecurityToken securityToken = securityTokenHandler.CreateToken(TokenDescriptior);
                Token=securityTokenHandler.WriteToken(securityToken);
            }
            if (string.IsNullOrEmpty(Token))
            {
                return BadRequest("not found");
            }
            return Ok(Token);
            }
        }
}
