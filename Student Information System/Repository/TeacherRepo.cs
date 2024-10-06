using Student_Information_System.Models;
using Student_Information_System.Repository.Interfaces;
using Student_Information_System.Utility;
using Student_Information_System.Exceptions;
using System;
using System.Data.SqlClient;

namespace Student_Information_System.Repository
{
    internal class TeacherRepo : ITeacher
    {
        SqlCommand cmd = null;

        public TeacherRepo()
        {
            cmd = new SqlCommand();
        }

        public void UpdateTeacherInfo(int teacherId, string firstName, string lastName, string email, string expertise)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(expertise))
                {
                    throw new InvalidTeacherDataException();
                }

                using (var connection = new SqlConnection(DbConnUtil.GetConnString()))
                {
                    connection.Open();

                    cmd = new SqlCommand("UPDATE Teacher SET first_name = @FirstName, last_name = @LastName, email = @Email, expertise = @Expertise WHERE teacher_id = @TeacherId", connection);
                    cmd.Parameters.AddWithValue("@TeacherId", teacherId);
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Expertise", expertise);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Teacher ID: {teacherId} updated successfully.");
                    }
                    else
                    {
                        throw new TeacherNotFoundException();
                    }
                }
            }
            catch (InvalidTeacherDataException)
            {
                throw;
            }
            catch (TeacherNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating teacher information: {ex.Message}");
            }
        }

        public void DisplayTeacherInfo(int teacherId)
        {
            try
            {
                using (var connection = new SqlConnection(DbConnUtil.GetConnString()))
                {
                    connection.Open();

                    cmd = new SqlCommand("SELECT first_name, last_name, email, expertise FROM Teacher WHERE teacher_id = @TeacherId", connection);
                    cmd.Parameters.AddWithValue("@TeacherId", teacherId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine($"Teacher ID: {teacherId}\nName: {reader["first_name"]} {reader["last_name"]}\nEmail: {reader["email"]}\nExpertise: {reader["expertise"]}");
                        }
                        else
                        {
                            throw new TeacherNotFoundException();
                        }
                    }
                }
            }
            catch (TeacherNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving teacher information: {ex.Message}");
            }
        }

        public void GetAssignedCourses(int teacherId)
        {
            try
            {
                using (var connection = new SqlConnection(DbConnUtil.GetConnString()))
                {
                    connection.Open();

                    cmd = new SqlCommand("SELECT course_id, course_name FROM Courses WHERE teacher_id = @TeacherId", connection);
                    cmd.Parameters.AddWithValue("@TeacherId", teacherId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            Console.WriteLine($"Courses assigned to Teacher ID {teacherId}:");
                            while (reader.Read())
                            {
                                Console.WriteLine($"Course ID: {reader["course_id"]}, Course Name: {reader["course_name"]}");
                            }
                        }
                        else
                        {
                            throw new CourseNotFoundException();
                        }
                    }
                }
            }
            catch (CourseNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving assigned courses: {ex.Message}");
            }
        }
    }
}
