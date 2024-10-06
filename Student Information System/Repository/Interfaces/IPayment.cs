using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Student_Information_System.Models;

namespace Student_Information_System.Repository.Interfaces
{
    internal interface IPayment
    {
        void GetStudent(int paymentId);
        void GetPaymentAmount(int paymentId);
        void GetPaymentDate(int paymentId);

        void AddPaymentInfo(int paymentId, Student student, double amount, DateTime paymentDate);
        void UpdatePaymentInfo(int paymentId, Student student, double amount, DateTime paymentDate);
        void DeletePaymentInfo(int paymentId);
        void DisplayPaymentsInfo(int paymentId);

    }
}
