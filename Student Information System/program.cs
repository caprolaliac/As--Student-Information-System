using System;
using System.Collections.Generic;
using System.Linq;
using Main;
using Student_Information_System;
using Student_Information_System.Models;
using Student_Information_System.Repository;
using Student_Information_System.Repository.Interfaces;

namespace Student_Information_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region prac Question 1
            /*
             1 . Create a program that checks if a student is eligible for a scholarship based on their GPA and extracurricular
                activities.
                 The eligibility criteria are:
                 GPA must be above 3.5.
                 The student must have participated in at least 2 extracurricular activities.
            */
            //Console.WriteLine("Enter GPA: ");
            //float gpa = (float)Convert.ToDouble(Console.ReadLine());
            //Console.WriteLine("Enter No. of Co-curricular Activities: ");
            //int cca = Convert.ToInt32(Console.ReadLine());
            //if (cca >= 2 && gpa > 3.5)
            //{
            //    Console.WriteLine("Eligible as per the criteria.");
            //}
            //else
            //{
            //    Console.WriteLine("Not Eligible as per the criteria.");
            //}
            #endregion
            #region prac Question 2
            /*
             2. Create a program that simulates student enrollment in courses.
                Display options such as "Check Available Courses,
                Enroll in Course," "Drop Course.
                Ask the student to enter their current course load, and for "Enroll in Course," prompt them to select a course
                and ensure they are not exceeding the maximum allowed courses (ex: 4 courses).
                Display appropriate messages for success or failure
            */
            // Menu
            //String[] courses = { "Software Engineering", "Python", "Micro Processor", "Calculus" };
            //List<string> enrolled = new List<string>();
            //bool exit = false;

            //while (!exit)
            //{
            //    Console.WriteLine("Options: ");
            //    Console.WriteLine("1: Check Available Courses");
            //    Console.WriteLine("2: Enroll In Course");
            //    Console.WriteLine("3: Drop Course");
            //    Console.WriteLine("4: Exit");
            //    Console.WriteLine("Enter the option: ");
            //    int opt = Convert.ToInt32(Console.ReadLine());

            //    switch (opt)
            //    {
            //        case 1:
            //            Console.WriteLine("Available Courses: ");
            //            foreach (var item in courses)
            //            {
            //                Console.WriteLine(item);
            //            }
            //            break;

            //        case 2:
            //            if (enrolled.Count < 4) // Max 4 courses
            //            {
            //                Console.WriteLine("Enroll - Name of the Course: ");
            //                string enrollCourse = Console.ReadLine();
            //                if (courses.Contains(enrollCourse) && !enrolled.Contains(enrollCourse))
            //                {
            //                    enrolled.Add(enrollCourse);
            //                    Console.WriteLine("Course Enrolled");
            //                }
            //                else
            //                {
            //                    Console.WriteLine("Course Not Available or Already Enrolled.");
            //                }
            //            }
            //            else
            //            {
            //                Console.WriteLine("Maximum course limit reached.");
            //            }
            //            break;

            //        case 3:
            //            Console.WriteLine("Drop - Name of the Course: ");
            //            string dropCourse = Console.ReadLine();
            //            if (enrolled.Contains(dropCourse))
            //            {
            //                enrolled.Remove(dropCourse);
            //                Console.WriteLine("The course has been dropped.");
            //            }
            //            else
            //            {
            //                Console.WriteLine("You are not enrolled in this course.");
            //            }
            //            break;

            //        case 4:
            //            exit = true;
            //            Console.WriteLine("Exiting...");
            //            break;

            //        default:
            //            Console.WriteLine("Invalid option, please try again.");
            //            break;
            //    }
            //}
            #endregion
            #region prac Question 3
            /*
            3. Create a C# program that simulates a student information system with multiple student records.
            Use a loop to repeatedly ask the user for their student ID until they enter a valid student ID.
            Validate the student ID entered by the user.
            If the student ID is valid, display the student's grades. If not, ask the user to try again
            */
            //var studentInfo = new Dictionary<int, string>
            //{
            //    { 1, "Grade S" },
            //    { 2, "Grade F" },
            //    { 3, "Grade B" }
            //};

            //while (true)
            //{
            //    Console.WriteLine("Enter Student ID: ");
            //    int studentId = Convert.ToInt32(Console.ReadLine());

            //    if (studentInfo.ContainsKey(studentId))
            //    {
            //        Console.WriteLine($"Valid Id: Your Grade is: {studentInfo[studentId]}");
            //        break; // Exit the loop if valid
            //    }
            //    else
            //    {
            //        Console.WriteLine("Invalid ID. Please try again.");
            //    }
            //}
            #endregion
            #region student

            //StudentRepo studentRepo = new StudentRepo();
            //studentRepo.DisplayStudentInfo(1004);
            //studentRepo.DisplayAllStudentInfo();
            //studentRepo.AddStudents(1004, "KK", "GG", new DateTime(2002, 6, 4), "kkgg@123.com", "934567890");
            //studentRepo.DisplayStudentInfo(2);
            //studentRepo.DisplayStudentInfo(3);
            //studentRepo.UpdateStudentInfo(1004, "ab", "cd", new DateTime(2000,5,7),"abcd@abc.com","879876689");
            //studentRepo.DisplayStudentInfo(1004);
            //studentRepo.MakePayment(1004, 3000, new DateTime(2024,5,12));
            //studentRepo.DisplayStudentInfo(3);
            //studentRepo.DeleteStudent(3);
            //studentRepo.DisplayStudentInfo(3);

            #endregion
            #region course

            //CourseRepo courseRepo = new CourseRepo();
            //courseRepo.AddCourses(3,"DSA","1003","Danny Rick");
            //courseRepo.DisplayCourseInfo(1);
            //courseRepo.DisplayCourseInfo(2);
            //courseRepo.DisplayCourseInfo(3);
            //courseRepo.UpdateCourseInfo("1003","c#","Amx Rev");
            //courseRepo.DisplayCourseInfo(3);
            //courseRepo.DeleteCourseInfo("1003", "c#", "Amx Rev");
            //courseRepo.GetTeacher(1);
            //CourseRepo courseRepo = new CourseRepo();
            //courseRepo.DisplayAllCourseInfo();
            #endregion
            #region Payment

            //PaymentRepo paymentRepo = new PaymentRepo();
            //paymentRepo.GetStudent(1);
            //paymentRepo.GetStudent(2);
            //paymentRepo.GetStudent(3);
            //paymentRepo.GetPaymentAmount(1);
            //paymentRepo.GetPaymentDate(1);
            //paymentRepo.GetPaymentDate(2);
            //var studentToAddPayment = TestData.Students[1];
            //paymentRepo.AddPaymentInfo(4, studentToAddPayment, 1500, new DateTime(2024, 4, 15));
            //paymentRepo.DisplayPaymentsInfo(4);
            //paymentRepo.UpdatePaymentInfo(4, studentToAddPayment, 3500, new DateTime(2025, 5, 16));
            //paymentRepo.DisplayPaymentsInfo(4);

            #endregion
            //To DO : 
            //menu
            SIS studentInformationSystem = new SIS();
            studentInformationSystem.Run();
        }
    }
}
