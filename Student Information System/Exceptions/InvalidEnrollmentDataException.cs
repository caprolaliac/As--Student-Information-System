using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Exceptions
{
    internal class InvalidEnrollmentDataException : Exception
    {
        public InvalidEnrollmentDataException()
            : base("The provided enrollment data is invalid.") { }
    }
}
