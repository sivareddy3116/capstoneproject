using System.ComponentModel.DataAnnotations;

namespace BlogApplication.Models
{
    public class login
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

