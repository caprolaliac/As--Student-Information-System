using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Models
{
    internal class Payment
    {
        public int PaymentId { get; set; }
        public Student Student { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        public Payment(int paymentId, Student student, double amount, DateTime paymentDate)
        {
            PaymentId = paymentId;
            Student = student;
            Amount = amount;
            PaymentDate = paymentDate;
        }

    }
}
