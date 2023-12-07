using ItemProjLast.Domian.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ItemProjLast.Controllers
{
    [Controller]
    public class LoginController:ControllerBase
    {
        private const string TokenSecret = "Space302";
        private static readonly TimeSpan TokenLifetime=TimeSpan.FromHours(10);

            


    }
}
