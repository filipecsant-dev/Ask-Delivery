using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Repository
{
    public interface INeighbohoodsRepository
    {
         Task<ActionResult<IEnumerable<Neighbohoods>>> GetAll();
         Task<ActionResult> Create([FromBody] Neighbohoods n);
         Task<ActionResult> Update(int id, [FromBody] Neighbohoods n);
         Task<ActionResult> Delete(int id);
    }
}