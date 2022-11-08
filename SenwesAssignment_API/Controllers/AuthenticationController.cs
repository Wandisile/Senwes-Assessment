using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SenwesAssignment_API.Models;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using SenwesAssignment_API.Confuguration;
using Microsoft.Extensions.Options;

namespace SenwesAssignment_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AppSettings _appSettings;

        public AuthenticationController(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
        }

        [HttpPost, Route("login")]
        public IActionResult Login(Login model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
                {
                    return BadRequest("Username and/or Password not specified");
                }

                if (model.Username.Equals(_appSettings.LoginDetails.Username) &&
                model.Password.Equals(_appSettings.LoginDetails.Password))
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT.Secret));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    
                    var jwtSecurityToken = new JwtSecurityToken(
                        issuer: _appSettings.JWT.ValidIssuer,
                        audience: _appSettings.JWT.ValidAudience,
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(60),
                        signingCredentials: signinCredentials
                    );
                    return Ok(new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));
                }
                return Unauthorized();
            }
            catch
            {
                return BadRequest
                ("An error occurred while generating the token");
            }
        }
    }
}
