using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Exceptions
{
    internal class InvalidTeacherDataException : Exception
    {
        public InvalidTeacherDataException()
            : base("The provided teacher data is invalid.") { }
    }
}
