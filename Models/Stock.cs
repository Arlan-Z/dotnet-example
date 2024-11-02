using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Stocks")]
    public class Stock
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18, 2)")] // Purchase attribute, points that in DB it will be saved as decimal accurate to 18 digits and 2 decimal places - 1234567890123456.78 as an example
        public decimal Purchase { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal LastDiv { get; set; }
        public string industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}