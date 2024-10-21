using System.ComponentModel.DataAnnotations.Schema;
using Models;

namespace Dtos.Stock
{
    public class StockDto
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }
        public string industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
        // Comments was here
    }
}