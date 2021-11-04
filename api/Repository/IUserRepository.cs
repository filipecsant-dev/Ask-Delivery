using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Repository
{
    public interface IUserRepository
    {
        ActionResult Get(long id);
        Task<ActionResult> Create([FromBody] Users u);
        Task<ActionResult> Update(long id, [FromBody] Users u);
    }
}