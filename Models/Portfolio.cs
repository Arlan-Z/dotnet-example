using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Portfolios")]
    public class Portfolio
    {
        public string AppUserId { get; set; } // by default identety specifies id as string
        public int StockId { get; set; }
        public AppUser AppUser { get; set; }
        public Stock Stock { get; set; }
    }
}