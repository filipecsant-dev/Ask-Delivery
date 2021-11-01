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






        /*
         Task<ActionResult> Create([FromBody] Users u);
        //IEnumerable<Users> GetAll(); (Pega todos usuarios)
        ActionResult Get(long id);
        Task<ActionResult> Update(long id, [FromBody] Users u);
        Task<ActionResult> Remove(long id);*/
    }
}