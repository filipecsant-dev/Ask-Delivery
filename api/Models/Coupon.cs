using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Coupon
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(20, ErrorMessage = "Máximo de 20 Caracteres permitido!")]
        public string Cupon { get; set; }
        
        [MaxLength(15)]
        public string Status { get; set; }
    }
}