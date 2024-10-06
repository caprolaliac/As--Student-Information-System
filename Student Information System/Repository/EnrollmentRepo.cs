using Student_Information_System.Models;
using Student_Information_System.Repository.Interfaces;
using Student_Information_System.Utility;
using Student_Information_System.Exceptions;
using System.Data.SqlClient;

namespace Student_Information_System.Repository
{
    internal class EnrollmentRepo : IEnrollment
    {
        SqlCommand cmd = null;

        public EnrollmentRepo()
        {
            cmd = new SqlCommand();
        }

        public void GetStudent(int enrollmentId)
        {
            try
            {
                if (enrollmentId <= 0)
                {
                    throw new InvalidEnrollmentDataException();
                }

                using (var connection = new SqlConnection(DbConnUtil.GetConnString()))
                {
                    connection.Open();
                    cmd = new SqlCommand("select count(*) from enrollments where enrollment_id = @EnrollmentId", connection);
                    cmd.Parameters.AddWithValue("@EnrollmentId", enrollmentId);
                    if ((int)cmd.ExecuteScalar() == 0)
                    {
                        throw new InvalidEnrollmentDataException();
                    }

                    cmd = new SqlCommand("select s.student_id, s.first_name, s.last_name from students s join enrollments e on s.student_id = e.student_id where e.enrollment_id = @EnrollmentId", connection);
                    cmd.Parameters.AddWithValue("@EnrollmentId", enrollmentId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine($"Enrollment ID: {enrollmentId}");
                            Console.WriteLine($"Student Name: {reader["first_name"]} {reader["last_name"]}");
                            Console.WriteLine($"Student ID: {reader["student_id"]}");
                        }
                        else
                        {
                            throw new StudentNotFoundException();
                        }
                    }
                }
            }
            catch (InvalidEnrollmentDataException)
            {
                throw;
            }
            catch (StudentNotFoundException)
            {
                throw;
            }
            catch (SqlException ex)
            {
                throw new Exception();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public void GetCourse(int enrollmentId)
        {
            try
            {
                if (enrollmentId <= 0)
                {
                    throw new InvalidEnrollmentDataException();
                }

                using (var connection = new SqlConnection(DbConnUtil.GetConnString()))
                {
                    connection.Open();
                    cmd = new SqlCommand("select count(*) from enrollments where enrollment_id = @EnrollmentId", connection);
                    cmd.Parameters.AddWithValue("@EnrollmentId", enrollmentId);
                    if ((int)cmd.ExecuteScalar() == 0)
                    {
                        throw new InvalidEnrollmentDataException();
                    }

                    cmd = new SqlCommand("select c.course_id, c.course_name, c.course_code from courses c join enrollments e on c.course_id = e.course_id where e.enrollment_id = @EnrollmentId", connection);
                    cmd.Parameters.AddWithValue("@EnrollmentId", enrollmentId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine($"Enrollment ID: {enrollmentId}");
                            Console.WriteLine($"Course Name: {reader["course_name"]}");
                            Console.WriteLine($"Course Code: {reader["course_code"]}");
                            Console.WriteLine($"Course ID: {reader["course_id"]}");
                        }
                        else
                        {
                            throw new CourseNotFoundException();
                        }
                    }
                }
            }
            catch (InvalidEnrollmentDataException)
            {
                throw;
            }
            catch (CourseNotFoundException)
            {
                throw;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error while retrieving course information: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving course information: {ex.Message}");
            }
        }

        private bool ValidateEnrollment(int enrollmentId, SqlConnection connection)
        {
            cmd = new SqlCommand("select count(*) from enrollments where enrollment_id = @EnrollmentId", connection);
            cmd.Parameters.AddWithValue("@EnrollmentId", enrollmentId);
            return (int)cmd.ExecuteScalar() > 0;
        }
    }
}
