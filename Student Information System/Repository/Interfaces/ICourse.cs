using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Repository.Interfaces
{
    internal interface ICourse
    {
        public void AddCourses(int courseId, string courseName, int credits, int teacherId, string courseCode);
        public void AssignTeacher(int courseId, int teacherId);
        public void DisplayCourseInfo(int courseId);
        public void DisplayAllCourseInfo();
        public void UpdateCourseInfo(int courseId,string courseName, string courseCode, int credits, int teacherId);
        public void DeleteCourseInfo(int courseId);
        public void GetEnrollments(int courseId);
        public void GenerateEnrollmentReport(int courseId);
        public void GetTeacher(int courseId);
    }
}
