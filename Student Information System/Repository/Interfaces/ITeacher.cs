using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Repository.Interfaces
{
    internal interface ITeacher
    {
        void UpdateTeacherInfo(int teacherId, string first_name, string last_name, string email, string expertise);
        void DisplayTeacherInfo(int teacherId);
        void GetAssignedCourses(int teacherId);
    }
}
