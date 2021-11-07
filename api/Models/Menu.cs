using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(50, ErrorMessage = "Escolha um nome mais curto.")]
        public string Type { get; set; }
        
        [MaxLength(100, ErrorMessage = "Escolha um nome mais curto.")]
        public string Product { get; set; }
        
        [MaxLength(150, ErrorMessage = "Escolha um nome mais curto.")]
        public string Description { get; set; }
        
        public float Value { get; set; }
        
        public string Image { get; set; }
    }
}