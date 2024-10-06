using Student_Information_System.Models;
using Student_Information_System.Repository.Interfaces;
using Student_Information_System;
using System.Data.SqlClient;
using Student_Information_System.Utility;
using Student_Information_System.Exceptions;

namespace Student_Information_System.Repository
{
    internal class StudentRepo : IStudent
    {
        public void AddStudents(int studentId, string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(email))
                {
                    throw new InvalidStudentDataException();
                }

                using (var connection = new SqlConnection(DbConnUtil.GetConnString()))
                {
                    connection.Open();

                    using (var cmd = new SqlCommand("select count(*) from students where email = @Email", connection))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        int exists = (int)cmd.ExecuteScalar();
                        if (exists > 0)
                        {
                            throw new InvalidStudentDataException();
                        }
                    }

                    using (var cmd = new SqlCommand("insert into students (first_name, last_name, date_of_birth, email, phone_number) values (@FirstName, @LastName, @DateOfBirth, @Email, @PhoneNumber)", connection))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        cmd.ExecuteNonQuery();
                    }
                }

                Console.WriteLine($"Student {firstName} {lastName} added successfully.");
            }
            catch (InvalidStudentDataException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new InvalidStudentDataException();
            }
        }

        public void DisplayStudentInfo(int studentId)
        {
            try
            {
                using (var connection = new SqlConnection(DbConnUtil.GetConnString()))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("select first_name, last_name, date_of_birth, email, phone_number from students where student_id = @StudentId", connection))
                    {
                        cmd.Parameters.AddWithValue("@StudentId", studentId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Console.WriteLine($"Student ID: {studentId}\nName: {reader["first_name"]} {reader["last_name"]}\nDate of Birth: {reader["date_of_birth"]:d}\nEmail: {reader["email"]}\nPhone Number: {reader["phone_number"]}\n");
                            }
                            else
                            {
                                throw new StudentNotFoundException();
                            }
                        }
                    }
                }
            }
            catch (StudentNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving student information: " + ex.Message);
            }
        }

        public void EnrollInCourse(int studentId, int courseId)
        {
            try
            {
                using (var connection = new SqlConnection(DbConnUtil.GetConnString()))
                {
                    connection.Open();

                    using (var cmd = new SqlCommand("select count(*) from students where student_id = @StudentId", connection))
                    {
                        cmd.Parameters.AddWithValue("@StudentId", studentId);
                        if ((int)cmd.ExecuteScalar() == 0)
                        {
                            throw new StudentNotFoundException();
                        }
                    }

                    using (var cmd = new SqlCommand("select count(*) from courses where course_id = @CourseId", connection))
                    {
                        cmd.Parameters.AddWithValue("@CourseId", courseId);
                        if ((int)cmd.ExecuteScalar() == 0)
                        {
                            throw new CourseNotFoundException();
                        }
                    }

                    using (var cmd = new SqlCommand("select count(*) from enrollments where student_id = @StudentId and course_id = @CourseId", connection))
                    {
                        cmd.Parameters.AddWithValue("@StudentId", studentId);
                        cmd.Parameters.AddWithValue("@CourseId", courseId);
                        if ((int)cmd.ExecuteScalar() > 0)
                        {
                            throw new DuplicateEnrollmentException();
                        }
                    }

                    using (var cmd = new SqlCommand("insert into enrollments (student_id, course_id, enrollment_date) values (@StudentId, @CourseId, @EnrollmentDate)", connection))
                    {
                        cmd.Parameters.AddWithValue("@StudentId", studentId);
                        cmd.Parameters.AddWithValue("@CourseId", courseId);
                        cmd.Parameters.AddWithValue("@EnrollmentDate", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }

                    Console.WriteLine($"Student {studentId} successfully enrolled in course {courseId}.");
                }
            }
            catch (StudentNotFoundException)
            {
                throw;
            }
            catch (CourseNotFoundException)
            {
                throw;
            }
            catch (DuplicateEnrollmentException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new InvalidEnrollmentDataException();
            }
        }

        public void MakePayment(int studentId, decimal amount, DateTime paymentDate)
        {
            try
            {
                if (amount <= 0)
                {
                    throw new PaymentValidationException();
                }

                using (var connection = new SqlConnection(DbConnUtil.GetConnString()))
                {
                    connection.Open();

                    using (var cmd = new SqlCommand("select count(*) from students where student_id = @StudentId", connection))
                    {
                        cmd.Parameters.AddWithValue("@StudentId", studentId);
                        if ((int)cmd.ExecuteScalar() == 0)
                        {
                            throw new StudentNotFoundException();
                        }
                    }

                    using (var cmd = new SqlCommand("insert into payments (student_id, amount, payment_date) values (@StudentId, @Amount, @PaymentDate)", connection))
                    {
                        cmd.Parameters.AddWithValue("@StudentId", studentId);
                        cmd.Parameters.AddWithValue("@Amount", amount);
                        cmd.Parameters.AddWithValue("@PaymentDate", paymentDate);
                        cmd.ExecuteNonQuery();
                    }
                }

                Console.WriteLine($"Payment of {amount:C} made by student ID: {studentId} on {paymentDate:d}.");
            }
            catch (StudentNotFoundException)
            {
                throw;
            }
            catch (PaymentValidationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new PaymentValidationException();
            }
        }

        public void UpdateStudentInfo(int studentId, string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(email))
                {
                    throw new InvalidStudentDataException();
                }

                using (var connection = new SqlConnection(DbConnUtil.GetConnString()))
                {
                    connection.Open();

                    using (var cmd = new SqlCommand("select count(*) from students where student_id = @StudentId", connection))
                    {
                        cmd.Parameters.AddWithValue("@StudentId", studentId);
                        if ((int)cmd.ExecuteScalar() == 0)
                        {
                            throw new StudentNotFoundException();
                        }
                    }

                    using (var cmd = new SqlCommand("update students set first_name = @FirstName, last_name = @LastName, date_of_birth = @DateOfBirth, email = @Email, phone_number = @PhoneNumber where student_id = @StudentId", connection))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        cmd.Parameters.AddWithValue("@StudentId", studentId);
                        cmd.ExecuteNonQuery();
                    }
                }

                Console.WriteLine($"Student {firstName} {lastName} details updated successfully.");
            }
            catch (StudentNotFoundException)
            {
                throw;
            }
            catch (InvalidStudentDataException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new InvalidStudentDataException();
            }
        }

        public void DeleteStudent(int studentId)
        {
            try
            {
                using (var connection = new SqlConnection(DbConnUtil.GetConnString()))
                {
                    connection.Open();

                    using (var cmd = new SqlCommand("select count(*) from students where student_id = @StudentId", connection))
                    {
                        cmd.Parameters.AddWithValue("@StudentId", studentId);
                        if ((int)cmd.ExecuteScalar() == 0)
                        {
                            throw new StudentNotFoundException();
                        }
                    }

                    using (var cmd = new SqlCommand("delete from students where student_id = @StudentId", connection))
                    {
                        cmd.Parameters.AddWithValue("@StudentId", studentId);
                        cmd.ExecuteNonQuery();
                    }

                    Console.WriteLine($"Student {studentId} deleted successfully.");
                }
            }
            catch (StudentNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete student: {ex.Message}");
            }
        }

        public void DisplayAllStudentInfo()
        {
            try
            {
                using (var connection = new SqlConnection(DbConnUtil.GetConnString()))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("select student_id, first_name, last_name, date_of_birth, email, phone_number from students", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Console.WriteLine($"Student ID: {reader["student_id"]}\n" +
                                                      $"Name: {reader["first_name"]} {reader["last_name"]}\n" +
                                                      $"Date of Birth: {reader["date_of_birth"]:d}\n" +
                                                      $"Email: {reader["email"]}\n" +
                                                      $"Phone Number: {reader["phone_number"]}\n" +
                                                      "----------------------------------------");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No students found.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving all student information: " + ex.Message);
            }
        }

        public void GetEnrolledCourses(int studentId)
        {
            try
            {
                using (var connection = new SqlConnection(DbConnUtil.GetConnString()))
                {
                    connection.Open();

                    using (var cmd = new SqlCommand("select c.course_id, c.course_name, c.course_description from enrollments e " +
                                                    "join courses c on e.course_id = c.course_id " +
                                                    "where e.student_id = @StudentId", connection))
                    {
                        cmd.Parameters.AddWithValue("@StudentId", studentId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                Console.WriteLine($"Courses enrolled by student ID: {studentId}");
                                while (reader.Read())
                                {
                                    Console.WriteLine($"Course ID: {reader["course_id"]}\n" +
                                                      $"Course Name: {reader["course_name"]}\n" +
                                                      $"Course Description: {reader["course_description"]}\n");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"No courses found for student ID: {studentId}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving courses for student ID {studentId}: " + ex.Message);
            }
        }

        public void GetPaymentHistory(int studentId)
        {
            try
            {
                using (var connection = new SqlConnection(DbConnUtil.GetConnString()))
                {
                    connection.Open();

                    using (var cmd = new SqlCommand("select payment_id, amount, payment_date from payments where student_id = @StudentId", connection))
                    {
                        cmd.Parameters.AddWithValue("@StudentId", studentId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                Console.WriteLine($"Payment history for student ID: {studentId}");
                                while (reader.Read())
                                {
                                    Console.WriteLine($"Payment ID: {reader["payment_id"]}\n" +
                                                      $"Amount: {reader["amount"]:C}\n" +
                                                      $"Payment Date: {reader["payment_date"]:d}\n");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"No payment history found for student ID: {studentId}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving payment history for student ID {studentId}: " + ex.Message);
            }
        }

    }
}
