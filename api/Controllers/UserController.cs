using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Repository;
using api.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace api.Controllers
{
    [Route("[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase, IUserRepository
    {
        private readonly DataContext dc;

        public UserController(DataContext context)
        {
            dc = context;
        }

        private string validationregister(string email)
        {
            Users u = dc.users
                      .Where(x => x.Email == email)
                      .AsNoTracking()
                      .FirstOrDefault();

            if(u == null) return null;
            return u.Email;
        }

        private bool verify(long id)
        {
            var u = dc.users
                        .Where(x => x.Id == id)
                        .AsNoTracking()
                        .FirstOrDefault();
            if(u == null) return false;
            else return true;
        }

        [HttpGet("{id:int}")]
        public ActionResult Get(long id)
        {
            if(User.Identity.Name != id.ToString()) return BadRequest(new {message = "Você não tem permissão para isto!"});

            Users u = dc.users.Find(id);
            if(u == null) return BadRequest(new {message = "Nenhum usuário encontrado."});
            return Ok(u);
        }

        [AllowAnonymous]
        [HttpPost]        
        public async Task<ActionResult> Create([FromBody] Users u)
        {
            var errors = ValidationModel.ValidationErrors(u);
            foreach(var error in errors) return BadRequest(error);

            string Email = validationregister(u.Email);

            if(Email == u.Email) return BadRequest(new {message = "Esse email já está cadastrado!"});

            dc.users.Add(u);
            await dc.SaveChangesAsync();
            u.Password = "";
            return Created("Objeto usuario", u);
        
            
        }
        
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(long id, [FromBody] Users u)
        {
            if(!verify(id)) return BadRequest(new {message = "Nenhum usuário encontrado."});
            //Libera Owner e Master para Put
            if(!User.IsInRole("Owner") && !User.IsInRole("Master"))
            {
                if(User.Identity.Name != id.ToString() || u.Id != id) return BadRequest(new {message = "Você não tem permissão para isto!"});
            }

            //Pega dados do usuário para atualizar e restringe Owner e Master para Put somente em Role
            if(User.IsInRole("Owner") || User.IsInRole("Master"))
            {
                if(u.Id != id) return BadRequest(new {message = "Dados informados não coincidem!"});

                string Role= u.Role;//Pega Role passada
                //Pega dados do usuario
                var dadosusuario = dc.users
                                    .Where(x => x.Id == id)
                                    .AsNoTracking()
                                    .FirstOrDefault();
                
                u = dadosusuario; //Salva dados do usuario no objeto para não precisar ter duas instancias de salvamento
                u.Role = Role; //Altera o Role do objeto para o que deseja alterar
            }

            dc.Entry(u).State = EntityState.Modified;
            await dc.SaveChangesAsync();
            return Ok();
        }
    }
}