using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Repository.Interfaces
{
    internal interface IEnrollment
    {
        void GetStudent(int enrollmentId);
        void GetCourse(int enrollmentId);
        
    }
}
