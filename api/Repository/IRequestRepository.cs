
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Repository
{
    public interface IRequestRepository
    {
        Task<ActionResult<IEnumerable<Request>>> GetAll();
        ActionResult Get(long id);
        Task<ActionResult> Create([FromBody] Request r);
        Task<ActionResult> Update(long id, [FromBody] Request r);
    }
}