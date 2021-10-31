using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Users
    {
        public Users()
        {
            if(Role == null)
                Role = "Common";
        }

        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Obrigatório o cadastro do email.")]
        [EmailAddress(ErrorMessage = "Informe um email válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Obrigatório o cadastro da senha.")]
        [MinLength(5, ErrorMessage = "Necessário no minimo 5 caracteres.")]
        [MaxLength(16, ErrorMessage = "É permitido no máximo 16 caracteres.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Obrigatório o cadastro do nome.")]
        [MinLength(5, ErrorMessage = "Necessário no minimo 5 caracteres.")]
        [MaxLength(30, ErrorMessage = "É permitido no máximo 30 caracteres.")]
        public string Name { get; set; }

        [MinLength(8, ErrorMessage = "Informe um telefone válido.")]
        [MaxLength(10, ErrorMessage = "Informe um telefone válido.")]
        public string Telephone { get; set; }

        [MaxLength(150)]
        public string Adress { get; set; }
        
        [MaxLength(10)]
        public string Role { get; set; }
    }
}