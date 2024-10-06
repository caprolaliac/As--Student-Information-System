using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Models
{
    internal class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<Enrollment> Enrollments { get; set; }

        //In the constructors of your classes, initialize the list or collection properties to create empty collections when an object is instantiated.
        public Student(int studentId, string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber)
        {
            StudentId = studentId;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            PhoneNumber = phoneNumber;
            Enrollments = new List<Enrollment>();
        }
    }
}
