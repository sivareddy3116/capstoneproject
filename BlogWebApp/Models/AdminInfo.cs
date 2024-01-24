using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogWebApp.Models
{
    [Table("AdminInfo")]
    public class AdminInfo
    {
        [Key]
        [Required(ErrorMessage = "The EmailId field is required.")]
        [EmailAddress(ErrorMessage = "The EmailId field is not a valid e-mail address.")]
        public string? EmailId { get; set; }
        [Required(ErrorMessage = "The Password field is required.")]
        public string? Password { get; set; }
    }
}
