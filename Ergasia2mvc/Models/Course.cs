using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ergasia2mvc.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        [StringLength(60)]
        public string CourseTitle { get; set; }

        [Required]
        [StringLength(45)]
        public string CourseSemester { get; set; }

        [Required]
        [StringLength(45)]
        public string ProfessorAFM { get; set; }

        [ForeignKey("ProfessorAFM")]
        public User user { get; set; }
    }
}
