using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Exceptions
{
    internal class DuplicateEnrollmentException : Exception
    {
        public DuplicateEnrollmentException()
            : base("The student is already enrolled in this course.") { }

    }
}
