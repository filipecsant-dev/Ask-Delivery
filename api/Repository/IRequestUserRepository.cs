using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Repository
{
    public interface IRequestUserRepository
    {
         ActionResult<IEnumerable<RequestUser>> Get(long id);
    }
}