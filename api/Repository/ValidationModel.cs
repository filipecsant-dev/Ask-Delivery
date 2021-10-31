using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api.Repository
{
    public class ValidationModel
    {
        public static IEnumerable<ValidationResult> ValidationErrors(object obj)
        {
            var resultadoValidacao = new List<ValidationResult>();
            var contexto = new ValidationContext(obj, null, null);
            Validator.TryValidateObject(obj, contexto, resultadoValidacao , true);
            return resultadoValidacao ;
        }
    }
}