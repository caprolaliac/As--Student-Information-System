using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Models
{

    internal class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string Instructor { get; set; }
        List<Enrollment> Enrollments { get; set; }

        public Course(int courseId, string courseName, string courseCode, string instructor)
        {
            CourseId = courseId;
            CourseName = courseName;
            CourseCode = courseCode;
            Instructor= instructor;
            Enrollments = new List<Enrollment>();
        }

    }

}
