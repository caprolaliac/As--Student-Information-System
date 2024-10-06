using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Exceptions
{
    internal class InvalidStudentDataException : Exception
    {
        public InvalidStudentDataException()
            : base("The provided student data is invalid.") { }
    }
}
