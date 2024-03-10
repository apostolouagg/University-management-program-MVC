using System.ComponentModel.DataAnnotations;

namespace Ergasia2mvc.Models
{
    public class AddProfessorViewModel
    {
        [Key]
        public string AFM { get; set; }

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
        public string ProfessorUsername { get; set; }

        public string Password { get; set; }
        public string secName { get; set; }
    }
}
