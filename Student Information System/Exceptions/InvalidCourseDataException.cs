using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Exceptions
{
    internal class InvalidCourseDataException : Exception
    {
        public InvalidCourseDataException()
            : base("The provided course data is invalid.") { }
    }
}
