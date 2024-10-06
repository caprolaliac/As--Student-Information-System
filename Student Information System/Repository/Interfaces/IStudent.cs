using Student_Information_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Repository.Interfaces
{
    internal interface IStudent
    {
        public void AddStudents(int studentId, string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber);
        public void DisplayStudentInfo(int studentId);
        public void DisplayAllStudentInfo();
        void EnrollInCourse(int studentId, int courseId);
        void UpdateStudentInfo(int studentId, string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber);
        void DeleteStudent(int studentId);
        void MakePayment(int studentId, decimal amount, DateTime paymentDate);
        void GetEnrolledCourses(int studentId);
        void GetPaymentHistory(int studentId);

    }
}
