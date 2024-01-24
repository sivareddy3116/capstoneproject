using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogWebApp.Models
{
    [Table("BlogInfo")]
    public class BlogInfo
    {
        [Key]
        public int BlogId { get; set; }
        [Required(ErrorMessage = "The Title field is required.")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "The Subject field is required.")]
        public string? Subject { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string? BlogUrl { get; set; }
        [Required(ErrorMessage = "The EmpEmailId field is required.")]
        public string? EmpEmailId { get; set; }
    }
}