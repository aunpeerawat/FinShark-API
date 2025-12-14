using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Stock
{
    public class UpdateStockRequestDto
    {
        [Required]
        [MaxLength(10,ErrorMessage="Symbol cannot be more than 10 characters")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(20,ErrorMessage="Company cannot be more than 20 characters")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1,100000000000)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001,100)]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(20,ErrorMessage="Industry cannot be more than 20 characters")]
        public string Industry { get; set; } = string.Empty;
        [Required]
        [Range(1,500000000000)]
        public long MarketCap { get; set; }
    }
}