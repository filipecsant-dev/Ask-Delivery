using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Neighbohoods
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "Escolha um nome mais curto.")]
        public string District { get; set; }
        public float Value { get; set; }
    }
}