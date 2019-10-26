using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using FitHub.WebApp.Data;
using FitHub.WebApp.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FitHub.WebApp.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly AuthOptions options;
        private readonly DataBaseContext dataBase;
        private readonly IConfiguration configuration;

        public AuthController(IOptions<AuthOptions> options,
            DataBaseContext dataBase,
            IConfiguration configuration)
        {
            this.options = options.Value;
            this.dataBase = dataBase;
            this.configuration = configuration;
        }

        [HttpPost]
        public IActionResult Authorize([FromBody] AuthorizeRequest request) =>
             Json(new { Token = JwtCreate(request) });

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("test")]
        public IActionResult Get()
        {
            return Json("cdg");
        }

        private ClaimsIdentity GetIdentity(AuthorizeRequest model)
        {
            var result = dataBase.Users.FirstOrDefault(x => x.Email == model.Login);
            if (result == null)
            {
                return null;
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, model.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, model.Password)
            };

            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;

        }
        private string JwtCreate(AuthorizeRequest request)
        {
            var now = DateTime.Now;
            string encodedJwt;
            var jwtToken = new JwtSecurityToken
                (
                    issuer: configuration.GetSection("AuthOptions")["ISSUER"],
                    audience: configuration.GetSection("AuthOptions")["AUDIENCE"],
                    notBefore: now,
                    claims: GetIdentity(request).Claims,
                    expires: now.Add(TimeSpan.FromMinutes(double.Parse(configuration.GetSection("AuthOptions")["LIFETIME"]))),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(configuration.GetSection("AuthOptions")["KEY"]), SecurityAlgorithms.HmacSha256)
                );

            return encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}