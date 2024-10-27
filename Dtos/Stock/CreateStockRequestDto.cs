using System.ComponentModel.DataAnnotations;

namespace Dtos.Stock
{
    public class CreateStockRequestDto
    {
        [Required]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "The {0} must be exactly {1} characters long.")] // 0 - Object, 1 - first parameter, 2 - second parameter
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(15, ErrorMessage = "{0}: Maximum 15 characters ")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1, 1000000)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.0001, 100)]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "{0}: Maximum 20 characters")]
        public string industry { get; set; } = string.Empty;
        [Required]
        [Range(1, 50000000)]
        public long MarketCap { get; set; }
    }
}
