using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Repository
{
    public interface IMenuRepository
    {
        Task<ActionResult<IEnumerable<Menu>>> GetAll();

        Task<ActionResult> Create([FromBody] Menu m);

        Task<ActionResult> Update([FromBody] Menu m, int id);

        Task<ActionResult> Delete(int id);


    }
}