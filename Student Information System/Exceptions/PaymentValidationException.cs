﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Exceptions
{
    internal class PaymentValidationException : Exception
    {
        public PaymentValidationException()
            : base("Payment not Validated.") { }

    }
}
