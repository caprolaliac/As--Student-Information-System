using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Exceptions
{
    internal class InsufficientFundsException : Exception
    {
        public InsufficientFundsException()
            : base("Insufficient funds for enrollment.") { }
    }
}
