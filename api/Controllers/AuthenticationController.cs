using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using api.Models;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;

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
                return BadRequest(new { message = "Usuário não existe!"});
            }
            //Validação
            if(request.Email == u.Email && request.Password == u.Password)
            {
                //Gerar token
                //Definindo as claims
                var claims = new[]
                {
                   new Claim(ClaimTypes.Email, u.Email),
                   new Claim(ClaimTypes.Name, u.Id.ToString()),
                   new Claim(ClaimTypes.Role, u.Role)
                   //new Claim(ClaimTypes.Role, "Admin")//Passando função do usuario
                };

               
                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));

                
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //Gerar o token
                var token = new JwtSecurityToken(
                    issuer: "localhost",//Qm é o emissor
                    audience: "localhost",//Qm é a audiencia
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