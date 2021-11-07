using System.Security.AccessControl;
using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using api.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq;

namespace api.Controllers
{
    [Route("[Controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RequestController : ControllerBase, IRequestRepository
    {
        private readonly DataContext dc;
        public RequestController(DataContext context)
        {
            dc = context;
        }

        private bool verify(long id)
        {
            var r = dc.request
                        .Where(x => x.Id == id)
                        .AsNoTracking()
                        .FirstOrDefault();
            if(r == null) return false;
            else return true;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Request r)
        {
            dc.request.Add(r);
            await dc.SaveChangesAsync();
            return Created("Objeto request",r);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Request> Get(long id)
        {
            Request request = dc.request
                            .Where(x => x.Id == id)
                            .AsNoTracking()
                            .Include(x => x.RequestList)
                            .FirstOrDefault();

            if(request == null) return BadRequest(new {message = "Nenhum pedido encontrado!"});

            return Ok(request);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetAll()
        { 
            var request = await dc.request
                            .AsNoTracking()
                            .Include(x => x.RequestList)
                            .ToListAsync();
                            
            return Ok(request);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(long id, [FromBody] Request r)
        {
            if(!verify(id)) return BadRequest(new {message = "Pedido não existe."});
            if(id != r.Id) return BadRequest(new {message = "Solicitação inválida."});

            dc.Entry(r).State = EntityState.Modified;
            await dc.SaveChangesAsync();
            return Ok();
        }
    }
}