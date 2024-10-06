using Student_Information_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System
{
    class TestData
    {
        //syntax: var students = new List<Student>()
        public static List<Student> Students = new List<Student>
        {
            new Student(1, "Varun","G",new DateTime(2003,5,3),"varun@abs.com","3898483984"),
            new Student(2, "raj","dk",new DateTime(1990,12,4),"rajdk@abs.com","4798483984")
        };
        public static List<Course> Courses = new List<Course>
        {
            new Course(1, "Java", "1001", "Ram Raj"),
            new Course(2, "Python", "1002", "Krish Raj")
        };
        public static List<Teacher> Teachers = new List<Teacher>
        {
            new Teacher(201, "Ram", "Raj", "ramraj@abc.com"),
            new Teacher(202, "Krish", "Raj", "krishraj@abc.com")
        };
        public static List<Payment> Payments = new List<Payment>
        {
            new Payment(1, Students[0],3000,new DateTime(2024,1,23)),
            new Payment(2, Students[1],2000,new DateTime(2024,3,12))
        };

    }
}
