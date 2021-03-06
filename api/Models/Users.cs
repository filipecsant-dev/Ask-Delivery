using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace api.Models
{
    public class Users
    {
        public Users()
        {
            if(Role == null) Role = "Common";
        }

        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Obrigatório o cadastro do email.")]
        [EmailAddress(ErrorMessage = "Informe um email válido.")]
        [MaxLength(150, ErrorMessage = "Informe um email válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Obrigatório o cadastro da senha.")]
        [MaxLength(250, ErrorMessage = "Escolha uma senha mais curta.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Obrigatório o cadastro do nome.")]
        [MinLength(5, ErrorMessage = "Nome muito curto.")]
        [MaxLength(100, ErrorMessage = "Seu nome esta muito grande! Abrevie.")]
        public string Name { get; set; }

        [MinLength(15, ErrorMessage = "Informe um telefone válido.")]
        [MaxLength(15, ErrorMessage = "Informe um telefone válido.")]
        public string Telephone { get; set; }

        [MaxLength(150)]
        public string Adress { get; set; }
        
        [MaxLength(10)]
        public string Role { get; set; }

        [JsonIgnore]
        public virtual ICollection<Request> Request { get; set; }
    }
}