using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[Authorize(Roles = "Employee, Owner")]
    public class MenuController : ControllerBase, IMenuRepository
    {
        private readonly DataContext dc;

        public MenuController(DataContext context)
        {
            dc = context;
        }

        private Menu filter(long id)
        {
            Menu m = dc.menu.Find(id);
            return m;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Menu m)
        {
            var errors = ValidationModel.ValidationErrors(m);
            foreach(var error in errors) return BadRequest(error);

            dc.menu.Add(m);
            await dc.SaveChangesAsync();
            return Created("Objeto Menu", m);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(long id)
        {
            Menu m = filter(id);
            if(m == null) return BadRequest(new {message = "Nenhum produto encontrado."});

            dc.menu.Remove(m);
            await dc.SaveChangesAsync();
            return Ok();
            
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Menu>>> GetAll()
        {
            var m = await dc.menu
                             .AsNoTracking()
                             .ToListAsync();

            return Ok(m);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update([FromBody] Menu m, long id)
        {
            Menu v = filter(id);
            if(v == null) return BadRequest(new {message = "Nenhum produto encontrado."}); 
            if(m.Id != id) return BadRequest(new {message = "Oops, algo deu errado."});
            
            dc.Entry(m).State = EntityState.Modified;
            await dc.SaveChangesAsync();
            return Ok();
        }
    }
}