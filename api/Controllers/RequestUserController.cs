using System.Collections.Generic;
using System.Linq;
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
    public class RequestUserController : ControllerBase, IRequestUserRepository
    {
        private readonly DataContext dc;

        public RequestUserController(DataContext context)
        {
            dc = context;
        }

        [HttpGet("{id:int}")]
        public ActionResult<RequestUser> Get(long id)
        {

            if(User.Identity.Name != id.ToString()) return BadRequest(new {message = "Você não tem permissão para isto!"});

            var u = dc.users
                        .AsNoTracking()
                        .Where(x => x.Id == id)
                        .Include(x => x.Request)
                        .ThenInclude(x => x.RequestList)
                        .FirstOrDefault();

            u.Password = "";

            return Ok(u);
        }
    }
}