using Student_Information_System.Models;

namespace Student_Information_System.Models
{
    internal class Enrollment
    {
        public int EnrollmentId { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public Enrollment(int enrollmentId ,Student student, Course course, DateTime enrollmentDate)
        {
            EnrollmentId = enrollmentId;
            Student = student;
            Course = course;
            EnrollmentDate = enrollmentDate;
        }

    }
}
