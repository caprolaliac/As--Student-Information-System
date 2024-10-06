using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Exceptions
{
    internal class TeacherNotFoundException : Exception
    {
        public TeacherNotFoundException()
            : base("The teacher was not found.") { }
    }
}
