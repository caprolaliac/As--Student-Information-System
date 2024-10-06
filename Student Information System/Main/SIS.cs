using System;
using Student_Information_System.Models;
using Student_Information_System.Repository;
using Student_Information_System.Repository.Interfaces;


namespace Main
{
    public class SIS
    {
        private readonly StudentRepo _studentRepo;
        private readonly CourseRepo _courseRepo;

        public SIS()
        {
            _studentRepo = new StudentRepo();
            _courseRepo = new CourseRepo();
        }

        public void Run()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("Student Information System ");
                Console.WriteLine("1. Student Management");
                Console.WriteLine("2. Course Management");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        StudentManagement();
                        break;

                    case 2:
                        CourseManagement();
                        break;

                    case 3:
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        private void StudentManagement()
        {
            Console.Clear();
            Console.WriteLine("Student Management");
            Console.WriteLine("1. Add New Student");
            Console.WriteLine("2. View Student Details");
            Console.WriteLine("3. Back to Main Menu");
            Console.Write("Enter your choice: ");

            int studentChoice = Convert.ToInt32(Console.ReadLine());

            switch (studentChoice)
            {
                case 1:
                    AddStudent();
                    break;

                case 2:
                    ViewStudent();
                    break;

                case 3:
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        private void AddStudent()
        {
            Console.WriteLine("Enter Student Details:");
            Console.Write("First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();
            Console.Write("Date of Birth (YYYY-MM-DD): ");
            DateTime dob = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Phone Number: ");
            string phone = Console.ReadLine();

            _studentRepo.AddStudents(0, firstName, lastName, dob, email, phone);
            Console.WriteLine("Student added successfully!");
        }

        private void ViewStudent()
        {
            Console.Write("Enter Student ID: ");
            int studentId = Convert.ToInt32(Console.ReadLine());
            _studentRepo.DisplayStudentInfo(studentId);
        }

        private void CourseManagement()
        {
            Console.Clear();
            Console.WriteLine("Course Management.");
            Console.WriteLine("1. Add New Course");
            Console.WriteLine("2. View Course Details");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("Enter your choice: ");

            int courseChoice = Convert.ToInt32(Console.ReadLine());

            switch (courseChoice)
            {
                case 1:
                    AddCourse();
                    break;

                case 2:
                    ViewCourse();
                    break;

                case 0:
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        private void AddCourse()
        {
            Console.WriteLine("Enter Course Details:");
            Console.Write("Course Name: ");
            string courseName = Console.ReadLine();
            Console.Write("Credits: ");
            int credits = Convert.ToInt32(Console.ReadLine());
            Console.Write("Teacher ID: ");
            int teacherId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Course Code: ");
            string courseCode = Console.ReadLine();

            _courseRepo.AddCourses(0, courseName, credits, teacherId, courseCode);
            Console.WriteLine("Course added successfully!");
        }

        private void ViewCourse()
        {
            Console.Write("Enter Course ID: ");
            int courseId = Convert.ToInt32(Console.ReadLine());
            _courseRepo.DisplayCourseInfo(courseId);
        }
    }
}
