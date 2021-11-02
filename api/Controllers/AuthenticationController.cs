using System;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using api.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using api.Data;
using System.Linq;

namespace api.Controllers
{
    [Route("[Controller]")]
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly DataContext dc;

        public AuthenticationController(IConfiguration configuration, DataContext context)
        {
            _configuration = configuration;
            dc = context;
        }

        [HttpPost]
        public IActionResult RequestToken([FromBody] Authentication request)
        {
           Users u = dc.users
                     .Where(x => x.Email == request.Email)
                     .FirstOrDefault();

            if(u == null)
            {
                return BadRequest(new { message = "Email não castrado!"});
            }

            //Validação
            if(request.Email == u.Email && request.Password == u.Password)
            {
                var claims = new[]
                {
                   new Claim(ClaimTypes.Email, u.Email),
                   new Claim(ClaimTypes.Name, u.Id.ToString()),
                   new Claim(ClaimTypes.Role, u.Role)
                };

               
                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));

                
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //Gerar o token
                var token = new JwtSecurityToken(
                    issuer: "localhost",
                    audience: "localhost",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: creds);

                return Ok(new
                {
                    message = "Permitido",
                    token = new JwtSecurityTokenHandler().WriteToken(token) 
                });

            }

            return BadRequest(new { message = "Usuário ou senha incorretos!"});
            
        }
        
    }
}