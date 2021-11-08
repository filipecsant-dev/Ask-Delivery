using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Repository
{
    public interface ICouponRepository
    {
         Task<ActionResult> Create([FromBody] Coupon c);

         Task<ActionResult> Delete(int id);
         
    }
}