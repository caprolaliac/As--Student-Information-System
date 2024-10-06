using Student_Information_System.Models;
using Student_Information_System.Repository.Interfaces;
using Student_Information_System.Utility;
using System.Data.SqlClient;
using Student_Information_System.Exceptions;
using System.Text;

namespace Student_Information_System.Repository
{
    internal class CourseRepo : ICourse
    {
        SqlCommand cmd = null;

        public CourseRepo()
        {
            cmd = new SqlCommand();
        }

        public void AddCourses(int courseId, string courseName, int credits, int teacherId, string courseCode)
        {
            #region with lists
            //var newCourse = new Course(courseId, courseName, courseCode, instructor);
            //courses.Add(newCourse);
            //Console.WriteLine($"Course {courseId} {courseName} added successfully.");
            #endregion
            using (var connection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                try
                {
                    connection.Open();
                    cmd = new SqlCommand("insert into Courses (course_id, course_name, course_code, credits, teacher_id) values (@CourseId, @CourseName, @CourseCode, @Credits, @TeacherId)", connection);
                    cmd.Parameters.AddWithValue("@CourseId", courseId);
                    cmd.Parameters.AddWithValue("@CourseName", courseName);
                    cmd.Parameters.AddWithValue("@CourseCode", courseCode);
                    cmd.Parameters.AddWithValue("@Credits", credits);
                    cmd.Parameters.AddWithValue("@TeacherId", teacherId);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine($"Added course {courseName} to teacher with Teacher Id:{teacherId}");
                }
                catch(CourseNotFoundException cex)
                {
                    Console.WriteLine(cex.Message);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            
        }

        public void DisplayCourseInfo(int courseId)
        {
            #region with lists
            //var course = courses.Find(c => c.CourseId == courseId);
            //if (course != null)
            //{
            //    Console.WriteLine($"Course Id: {course.CourseId}\nCourse Name: {course.CourseName}\nCourse Code: {course.CourseCode}\nInstructor: {course.Instructor}\n");
            //}
            //else
            //{
            //    Console.WriteLine("No course found with the provided ID.");
            //}
            #endregion
            using (var connection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                try
                {
                    connection.Open();
                    cmd = new SqlCommand("select c.course_name, c.course_code, c.credits, t.first_name AS t_first_name, t.last_name AS t_last_name FROM Courses c JOIN Teacher t ON c.teacher_id = t.teacher_id where c.course_id = @CourseId", connection);
                    cmd.Parameters.AddWithValue("@CourseId", courseId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine($"Course Details:\nCourse Id: {courseId}\nCourse Name: {reader["course_name"]}\nCourse Code: {reader["course_code"]}\nNo. Of Credits: {reader["credits"]}\nInstructor: {reader["t_first_name"]} {reader["t_last_name"]}");
                        }
                        else
                        {
                            Console.WriteLine($"Cannot update the course with the ID; {courseId}");
                        }
                    }
                }
                catch (CourseNotFoundException cex)
                {
                    Console.WriteLine(cex.Message);
                }
                catch (TeacherNotFoundException tex)
                {
                    Console.WriteLine(tex.Message);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void DisplayAllCourseInfo()
        {
            using (var connection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                try
                {
                    connection.Open();
                    cmd = new SqlCommand("select * from Courses", connection);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Course Id: {reader["course_id"]}\tCourse Name:{reader["course_name"]}\tCourse Code: {reader["course_code"]}\tCredits: {reader["credits"]}\t Teacher Id: {reader["teacher_id"]}");
                        }
                    }
                }
                catch(CourseNotFoundException cex)
                {
                    Console.WriteLine(cex.Message);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public void UpdateCourseInfo(int courseId,string courseName, string courseCode, int credits, int teacherId)
        {
            #region with lists
            //var course = courses.Find(c => c.CourseCode == courseCode || c.CourseName == courseName || c.Instructor == instructor);
            //if (course != null)
            //{
            //    course.CourseCode = courseCode;
            //    course.CourseName = courseName;
            //    course.Instructor = instructor;
            //    Console.WriteLine($"{course.CourseName} updated");
            //}
            //else
            //{
            //    Console.WriteLine("No course found to update.");
            //}
            #endregion
            using (var connection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                try
                {
                    connection.Open();
                    cmd = new SqlCommand("update Courses set course_name=@CourseName, course_code = @CourseCode, credits = @Credits, teacher_id = @TeacherId where course_id = @CourseId", connection);
                    cmd.Parameters.AddWithValue("@CourseId", courseId);
                    cmd.Parameters.AddWithValue("@CourseName", courseName);
                    cmd.Parameters.AddWithValue("@CourseCode", courseCode);
                    cmd.Parameters.AddWithValue("@Credits", credits);
                    cmd.Parameters.AddWithValue("@TeacherId", teacherId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Updated Course {courseName} successfully");
                    }
                    else
                    {
                        Console.WriteLine($"Could not update Course to {courseName}");
                    }
                }
                catch(InvalidCourseDataException invcex)
                {
                    Console.WriteLine(invcex.Message);
                }
                catch (CourseNotFoundException cex)
                {
                    Console.WriteLine(cex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void DeleteCourseInfo(int courseId)
        {
            #region using lists
            //var course = courses.Find(c => c.CourseCode == courseCode && c.CourseName == courseName && c.Instructor == instructor);
            //if (course != null)
            //{
            //    courses.Remove(course);
            //    Console.WriteLine($"{course.CourseName} removed");
            //}
            //else
            //{
            //    Console.WriteLine("No Course as per the details");
            //}
            #endregion
            using (var connection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                try
                {
                    connection.Open();
                    cmd = new SqlCommand("delete from Courses from when course_id=@CourseId", connection);
                    cmd.Parameters.AddWithValue("@CourseId", courseId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Deleted The course with Course Id {courseId} successfully");
                    }
                    else
                    {
                        Console.WriteLine($"Could not delete the Course with Course Id: {courseId}");
                    }
                }
                catch(CourseNotFoundException cex)
                {
                    Console.WriteLine(cex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void AssignTeacher(int courseId, int teacherId)
        {
            using (var connection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                try
                {
                    connection.Open();
                    cmd = new SqlCommand("update Courses set teacher_id = @TeacherId where course_id = @CourseId", connection);
                    cmd.Parameters.AddWithValue("@TeacherId", teacherId);
                    cmd.Parameters.AddWithValue("@CourseId", courseId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Teacher with ID {teacherId} assigned to Course {courseId}.");
                    }
                    else
                    {
                        Console.WriteLine("course not found to assign the teacher.");
                    }
                }
                catch(TeacherNotFoundException tex)
                {
                    Console.WriteLine(tex.Message);
                }
                catch(CourseNotFoundException cex)
                {
                    Console.WriteLine(cex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void GetEnrollments(int courseId)
        {
            //select e.student_id, s.first_name, s.last_name, e.enrollment_date from Enrollments e JOIN Students s on e.student_id = s.student_id where e.course_id = @CourseId
            using (var connection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                try
                {
                    connection.Open();
                    cmd = new SqlCommand("select e.student_id, s.first_name, s.last_name, e.enrollment_date from Enrollments e JOIN Students s on e.student_id = s.student_id where e.course_id = @CourseId", connection);
                    cmd.Parameters.AddWithValue("@CourseId", courseId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            Console.WriteLine($"Enrollments for Course ID: {courseId}:");
                            while (reader.Read())
                            {
                                Console.WriteLine($"Student ID: {reader["student_id"]}\nName: {reader["first_name"]} {reader["last_name"]}\nEnrollment Date: {reader["enrollment_date"]:d}\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"No enrollments found for Course ID {courseId}.");
                        }
                    }
                }
                catch (CourseNotFoundException cex)
                {
                    Console.WriteLine(cex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public void GenerateEnrollmentReport(int courseId)
        {
            try
            {
                using (var connection = new SqlConnection(DbConnUtil.GetConnString()))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("select e.student_id, s.first_name, s.last_name, e.enrollment_date from Enrollments e where e.course_id = @CourseId", connection))
                    {
                        cmd.Parameters.AddWithValue("@CourseId", courseId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                var report = new StringBuilder();
                                report.AppendLine($"Enrollment Report for Course ID: {courseId}\n");
                                report.AppendLine("Student ID | First Name | Last Name | Enrollment Date");
                                report.AppendLine(new string('-', 50));

                                while (reader.Read())
                                {
                                    report.AppendLine($"{reader["student_id"]} | {reader["first_name"]} | {reader["last_name"]} | {((DateTime)reader["enrollment_date"]).ToShortDateString()}");
                                }

                                 Console.WriteLine(report.ToString());
                            }
                            else
                            {
                                Console.WriteLine($"No enrollments found for Course ID: {courseId}.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating enrollment report: {ex.Message}");
            }
        }

        public void GetTeacher(int courseId)
        {
            #region with lists
            //var course = courses.FirstOrDefault(c => c.CourseId == courseId);
            //if (course != null)
            //{
            //    Console.WriteLine($"Instructor for {course.CourseId}.{course.CourseName} is {course.Instructor}");
            //}
            //else
            //{
            //    Console.WriteLine("No Instructor");
            //}

            #endregion

            using (var connection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                try
                {
                    connection.Open();
                    cmd = new SqlCommand("select t.teacher_id, t.first_name, t.last_name FROM Courses c join Teacher t on c.teacher_id = t.teacher_id where c.course_id = @CourseId", connection);
                    cmd.Parameters.AddWithValue("@CourseId", courseId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine($"Instructor for Course ID {courseId} is {reader["first_name"]} {reader["last_name"]} (ID: {reader["teacher_id"]})");
                        }
                        else
                        {
                            Console.WriteLine("No instructor found for the provided Course ID.");
                        }
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }
        }


    }
}
