using System.Collections.Generic;

namespace api.Models
{
 
    //SOMENTE PARA MODELO DE JSON SWAGGER
    public class RequestUser: Users
    {
        public ICollection<Request> Requests { get; }
    }
}