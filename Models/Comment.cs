using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Comments")]
    public class Comment
    {
        public int Id { get; set;}
        public string Title { get; set;} = string.Empty;
        public string Content { get; set;} = string.Empty;
        public DateTime CreatedOn {get; set;} = DateTime.Now;
        

        // Using convention we link Stock-Comment models
        // Convention means that entity framework dot net core
        // Search through code and form that relationship (one to many)
        public int? StockId { get; set; } // ? means can be null
        public Stock? Stock{ get; set; } // navigation property, allows us to be able to navigate within this relationship
                                        // so we can go like this Stock.id, Stock.Company and etc.
        public string AppUserId {get; set;}
        public AppUser appUser {get; set;}
    }
}