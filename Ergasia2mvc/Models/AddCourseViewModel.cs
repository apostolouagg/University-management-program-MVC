using System.ComponentModel.DataAnnotations;

namespace Ergasia2mvc.Models
{
    public class AddCourseViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        public string CourseTitle { get; set; }

        [Required]
        [StringLength(45)]
        public string CourseSemester { get; set; }

        [Required]
        [StringLength(45)]
        public string ProfessorAFM { get; set; }

        public string secName { get; set; }
    }
}
