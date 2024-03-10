using System.ComponentModel.DataAnnotations;

namespace Ergasia2mvc.Models
{
    public class GradeCourseViewModel
    {
        public int CourseId { get; set; }

        [Required]
        [StringLength(60)]
        public string CourseTitle { get; set; }

        [Required]
        [StringLength(45)]
        public string CourseSemester { get; set; }

        public string StudentId { get; set; }

        [Required]
        [StringLength(45)]
        public string Grade { get; set; }

        public string profName { get; set; }
    }
}
