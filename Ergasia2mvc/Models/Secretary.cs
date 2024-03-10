using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ergasia2mvc.Models
{
    public class Secretary
    {
        [Key]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(45)]
        public string Name { get; set; }

        [Required]
        [StringLength(45)]
        public string Surname { get; set; }

        [Required]
        [StringLength(45)]
        public string Department { get; set; }

        [Required]
        [StringLength(45)]
        public string SecretaryUsername { get; set; }

        [ForeignKey("SecretaryUsername")]
        public User user { get; set; }
    }
}
