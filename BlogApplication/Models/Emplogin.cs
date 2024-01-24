using System.ComponentModel.DataAnnotations;

namespace BlogApplication.Models
{
    public class Emplogin
    {
        [Required]
        [EmailAddress]
        public string EmailId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public int PassCode { get; set; }
    }
}
