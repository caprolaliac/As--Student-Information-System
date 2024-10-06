using Student_Information_System.Models;
using Student_Information_System.Repository.Interfaces;
using Student_Information_System.Utility;
using Student_Information_System.Exceptions;
using System.Data.SqlClient;

namespace Student_Information_System.Repository
{
    internal class PaymentRepo : IPayment
    {
        SqlCommand cmd = null;

        public PaymentRepo()
        {
            cmd = new SqlCommand();
        }

        public void GetStudent(int paymentId)
        {
            try
            {
                if (paymentId <= 0)
                {
                    throw new PaymentValidationException();
                }

                using (var connection = new SqlConnection(DbConnUtil.GetConnString()))
                {
                    connection.Open();

                    if (!ValidatePayment(paymentId, connection))
                    {
                        throw new PaymentValidationException();
                    }

                    cmd = new SqlCommand("select s.student_id, s.first_name, s.last_name, p.amount from payments p join students s on p.student_id = s.student_id where p.payment_id = @PaymentId", connection);
                    cmd.Parameters.AddWithValue("@PaymentId", paymentId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine($"Student Name: {reader["first_name"]} {reader["last_name"]}");
                            Console.WriteLine($"Amount Paid: {reader["amount"]:C}");
                        }
                        else
                        {
                            throw new StudentNotFoundException();
                        }
                    }
                }
            }
            catch (PaymentValidationException)
            {
                throw;
            }
            catch (StudentNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving student information: {ex.Message}");
            }
        }

        public void GetPaymentAmount(int paymentId)
        {
            try
            {
                if (paymentId <= 0)
                {
                    throw new PaymentValidationException();
                }

                using (var connection = new SqlConnection(DbConnUtil.GetConnString()))
                {
                    connection.Open();

                    if (!ValidatePayment(paymentId, connection))
                    {
                        throw new PaymentValidationException();
                    }

                    cmd = new SqlCommand("select amount from payments where payment_id = @PaymentId", connection);
                    cmd.Parameters.AddWithValue("@PaymentId", paymentId);

                    var amount = cmd.ExecuteScalar();
                    if (amount != null)
                    {
                        Console.WriteLine($"Amount for Payment ID {paymentId}: {((decimal)amount):C}");
                    }
                    else
                    {
                        throw new PaymentValidationException();
                    }
                }
            }
            catch (PaymentValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving payment amount: {ex.Message}");
            }
        }

        public void GetPaymentDate(int paymentId)
        {
            try
            {
                if (paymentId <= 0)
                {
                    throw new PaymentValidationException();
                }

                using (var connection = new SqlConnection(DbConnUtil.GetConnString()))
                {
                    connection.Open();

                    if (!ValidatePayment(paymentId, connection))
                    {
                        throw new PaymentValidationException();
                    }

                    cmd = new SqlCommand("select payment_date from payments where payment_id = @PaymentId", connection);
                    cmd.Parameters.AddWithValue("@PaymentId", paymentId);

                    var paymentDate = cmd.ExecuteScalar();
                    if (paymentDate != null)
                    {
                        Console.WriteLine($"Payment Date for Payment ID {paymentId}: {((DateTime)paymentDate):d}");
                    }
                    else
                    {
                        throw new PaymentValidationException();
                    }
                }
            }
            catch (PaymentValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving payment date: {ex.Message}");
            }
        }

        public void AddPaymentInfo(int paymentId, Student student, double amount, DateTime paymentDate)
        {
            try
            {
                if (student == null)
                {
                    throw new InvalidStudentDataException();
                }

                if (amount <= 0)
                {
                    throw new PaymentValidationException();
                }

                using (var connection = new SqlConnection(DbConnUtil.GetConnString()))
                {
                    connection.Open();

                    cmd = new SqlCommand("select count(*) from students where student_id = @StudentId", connection);
                    cmd.Parameters.AddWithValue("@StudentId", student.StudentId);
                    if ((int)cmd.ExecuteScalar() == 0)
                    {
                        throw new StudentNotFoundException();
                    }

                    cmd = new SqlCommand("insert into payments (student_id, amount, payment_date) values (@StudentId, @Amount, @PaymentDate)", connection);
                    cmd.Parameters.AddWithValue("@StudentId", student.StudentId);
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    cmd.Parameters.AddWithValue("@PaymentDate", paymentDate);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Payment of {amount:C} added for {student.FirstName} {student.LastName}");
                    }
                    else
                    {
                        throw new PaymentValidationException();
                    }
                }
            }
            catch (InvalidStudentDataException)
            {
                throw;
            }
            catch (StudentNotFoundException)
            {
                throw;
            }
            catch (PaymentValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding payment information: {ex.Message}");
            }
        }

        public void UpdatePaymentInfo(int paymentId, Student student, double amount, DateTime paymentDate)
        {
            try
            {
                if (paymentId <= 0)
                {
                    throw new PaymentValidationException();
                }

                if (student == null)
                {
                    throw new InvalidStudentDataException();
                }

                if (amount <= 0)
                {
                    throw new PaymentValidationException();
                }

                using (var connection = new SqlConnection(DbConnUtil.GetConnString()))
                {
                    connection.Open();

                    if (!ValidatePayment(paymentId, connection))
                    {
                        throw new PaymentValidationException();
                    }

                    cmd = new SqlCommand("update payments set student_id = @StudentId, amount = @Amount, payment_date = @PaymentDate where payment_id = @PaymentId", connection);
                    cmd.Parameters.AddWithValue("@StudentId", student.StudentId);
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    cmd.Parameters.AddWithValue("@PaymentDate", paymentDate);
                    cmd.Parameters.AddWithValue("@PaymentId", paymentId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Payment ID {paymentId} updated successfully.");
                    }
                    else
                    {
                        throw new PaymentValidationException();
                    }
                }
            }
            catch (PaymentValidationException)
            {
                throw;
            }
            catch (InvalidStudentDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating payment information: {ex.Message}");
            }
        }

        public void DeletePaymentInfo(int paymentId)
        {
            try
            {
                if (paymentId <= 0)
                {
                    throw new PaymentValidationException();
                }

                using (var connection = new SqlConnection(DbConnUtil.GetConnString()))
                {
                    connection.Open();

                    if (!ValidatePayment(paymentId, connection))
                    {
                        throw new PaymentValidationException();
                    }

                    cmd = new SqlCommand("delete from payments where payment_id = @PaymentId", connection);
                    cmd.Parameters.AddWithValue("@PaymentId", paymentId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Payment ID {paymentId} deleted successfully.");
                    }
                    else
                    {
                        throw new PaymentValidationException();
                    }
                }
            }
            catch (PaymentValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting payment information: {ex.Message}");
            }
        }

        public void DisplayPaymentsInfo(int paymentId)
        {
            try
            {
                if (paymentId <= 0)
                {
                    throw new PaymentValidationException();
                }

                using (var connection = new SqlConnection(DbConnUtil.GetConnString()))
                {
                    connection.Open();

                    if (!ValidatePayment(paymentId, connection))
                    {
                        throw new PaymentValidationException();
                    }

                    cmd = new SqlCommand("select p.payment_id, s.first_name, s.last_name, p.amount, p.payment_date from payments p join students s on p.student_id = s.student_id where p.payment_id = @PaymentId", connection);
                    cmd.Parameters.AddWithValue("@PaymentId", paymentId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine($"Payment ID: {reader["payment_id"]}");
                            Console.WriteLine($"Student Name: {reader["first_name"]} {reader["last_name"]}");
                            Console.WriteLine($"Amount: {reader["amount"]:C}");
                            Console.WriteLine($"Payment Date: {((DateTime)reader["payment_date"]):d}\n");
                        }
                        else
                        {
                            throw new PaymentValidationException();
                        }
                    }
                }
            }
            catch (PaymentValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error displaying payment information: {ex.Message}");
            }
        }

        private bool ValidatePayment(int paymentId, SqlConnection connection)
        {
            cmd = new SqlCommand("select count(*) from payments where payment_id = @PaymentId", connection);
            cmd.Parameters.AddWithValue("@PaymentId", paymentId);
            return (int)cmd.ExecuteScalar() > 0;
        }
    }
}
