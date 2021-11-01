using System.Threading.Tasks;
using api.Models;
using api.Repository;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase, IUserRepository
    {
        private readonly DataContext dc;

        public UserController(DataContext context)
        {
            this.dc = context;
        }
        

        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            if(User.Identity.Name != id.ToString()) return BadRequest(new {message = "Você não tem permissão para isto!"});

            Users u = dc.users.Find(id);
            return Ok(u);
        }

        [AllowAnonymous]
        [HttpPost]        
        public async Task<ActionResult> Create([FromBody] Users u)
        {
            var errors = ValidationModel.ValidationErrors(u);
            foreach(var error in errors) return BadRequest(error);

            dc.users.Add(u);
            await dc.SaveChangesAsync();
            u.Password = "";
            return Created("Usuário criado", u);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(long id, [FromBody] Users u)
        {
            if(User.Identity.Name != id.ToString()) return BadRequest(new {message = "Você não tem permissão para isto!"});
            
            dc.Entry(u).State = EntityState.Modified;
            await dc.SaveChangesAsync();
            return NoContent();
        }
    }
}