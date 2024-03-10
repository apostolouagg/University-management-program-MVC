using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ergasia2mvc.Models
{
    public class CourseHasStudents
    {
        public int CourseID { get; set; }
        public string StudentID { get; set; }
        public string GradeCourseStudent { get; set; }

        [ForeignKey("CourseID")]
        public Course course { get; set; }

        [ForeignKey("StudentID")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Student student { get; set; }
    }
}
