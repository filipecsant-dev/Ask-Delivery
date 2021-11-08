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
    [Authorize(Roles = "Empolyee, Owner")]
    public class CouponController : ControllerBase, ICouponRepository
    {
        private readonly DataContext dc;

        public CouponController(DataContext context)
        {
            dc = context;
        }

        private bool verify(string cupon)
        {
            var c = dc.coupon
                      .Where(x => x.Cupon == cupon)
                      .AsNoTracking()
                      .FirstOrDefault();

            if(c == null) return false;
            else return true;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Coupon c)
        {
            if(!verify(c.Cupon)) return BadRequest(new {message = "Este cupom já existe!"});

            var errors = ValidationModel.ValidationErrors(c);
            foreach(var error in errors) return BadRequest(error);

            dc.coupon.Add(c);
            await dc.SaveChangesAsync();
            return Created("Objeto Coupon", c);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            Coupon c = dc.coupon.Find(id);

            dc.coupon.Remove(c);
            await dc.SaveChangesAsync();
            return Ok();
        }
    }
}