using System.ComponentModel.DataAnnotations;

namespace Ergasia2mvc.Models
{
    public class AddStudentViewModel
    {
        [Key]
        public string RegistrationNumber { get; set; }

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
        public string StudentUsername { get; set; }

        public string Password { get; set; }

        public string secName { get; set; }
    }
}
