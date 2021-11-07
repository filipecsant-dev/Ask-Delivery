using System.Collections.Generic;
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
    [Authorize(Roles = "Employee, Owner")]
    public class NeighbohoodsController : ControllerBase, INeighbohoodsRepository
    {
        private readonly DataContext dc;

        public NeighbohoodsController(DataContext context)
        {
            dc = context;
        }

        private Neighbohoods filter(int id)
        {
            Neighbohoods n = dc.neighbohoods.Find(id);
            return n;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Neighbohoods n)
        {
            dc.neighbohoods.Add(n);
            await dc.SaveChangesAsync();
            return Created("Objeto Bairro", n);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            Neighbohoods n = filter(id);
            if(n == null) return BadRequest(new {message = "Nenhum bairro encontrado."});

            dc.neighbohoods.Remove(n);
            await dc.SaveChangesAsync();
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Neighbohoods>>> GetAll()
        {
            var n = await dc.neighbohoods
                               .AsNoTracking()
                               .ToListAsync();

            return Ok(n);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] Neighbohoods n)
        {
            Neighbohoods v = filter(id);
            if(v == null) return BadRequest(new {message = "Nenhum bairro encontrado!"});
            if(n.Id != id) return BadRequest(new {message = "Oops, algo deu errado."});

            dc.Entry(n).State = EntityState.Modified;
            await dc.SaveChangesAsync();
            return Ok();
        }
    }
}