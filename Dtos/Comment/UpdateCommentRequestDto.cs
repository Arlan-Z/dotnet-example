using System.ComponentModel.DataAnnotations;

namespace Dtos.Comment
{
    public class UpdateCommentRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title: Minimum 5 characters")]
        [MaxLength(100, ErrorMessage = "Title: Maximum 100 characters")]
        public string Title { get; set;} = string.Empty;

        
        [Required]
        [MinLength(10, ErrorMessage = "Content: Minimum 10 characters")]
        [MaxLength(256, ErrorMessage = "Content: Maximum 256 characters")]
        public string Content { get; set;} = string.Empty;
    }
}