using System.ComponentModel.DataAnnotations;

namespace Ergasia2mvc.Models
{
    public class User
    {
        [Key]
        [StringLength(45)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [StringLength(45)]
        public string Role { get; set; }
    }
}
