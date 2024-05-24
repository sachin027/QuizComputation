using QuizComputation.Model.CustomModel;
using QuizComputation.Model.DBContext;
using QuizComputation_Helper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace QuizComputation.Model.GenericRepository
{
    public class GenericRepository
    {
        public static bool IsSignUpValid(string commandText, Dictionary<string, object> parameters)
        {
            DataTable dataTable = new DataTable();
            bool isSignUpValid = false;
            using (QuizComputation_452Entities _context = new QuizComputation_452Entities())
            {
                string connectionString = _context.Database.Connection.ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 120;

                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                            }
                        }

                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int SignUpInt))
                        {
                            isSignUpValid = SignUpInt == 1;
                        }
                    }
                }
            }

            return isSignUpValid;
        }

        public static bool IsEmailExist(string commandText, Dictionary<string, object> parameters)
        {
            bool newRecord = false;
            using (QuizComputation_452Entities _context = new QuizComputation_452Entities())
            {
                string connectionString = _context.Database.Connection.ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 120;

                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                            }
                        }

                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int EmailExistsInt))
                        {
                            newRecord = EmailExistsInt == 1;
                        }
                    }
                }
            }

            return newRecord;
        }

        public static UserModel IsUserExist(string commandText, Dictionary<string, object> parameters)
        {
            UserModel _userModel = new UserModel();
            using (QuizComputation_452Entities _context = new QuizComputation_452Entities())
            {
                string connectionString = _context.Database.Connection.ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 120;

                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                            }
                        }

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    string username = reader["Username"].ToString();
                                    string email = reader["Email"].ToString();
                                    int user_id = DBHelper.ConvertStringToInt(reader["User_id"].ToString());
                               
                                    // Create an object to hold the values
                                    var user = new { Username = username, Email = email, User_Id = user_id };

                                    // Add the object to a list or process it as needed
                                    // In this example, I'm simply returning the first user found
                                    _userModel.User_id = user_id;
                                    _userModel.Username = username;
                                    _userModel.Email = email;

                                    // If you want to return multiple users, you can store them in a list
                                    // and return the list after the loop
                                   // userList.Add(user);
                                }
                            }
                        }
                    }
                }
            }

            return _userModel;
        }

        public static AdminModel IsAdminExist(string commandText, Dictionary<string, object> parameters)
        {
            AdminModel _adminModel = new AdminModel();
            using (QuizComputation_452Entities _context = new QuizComputation_452Entities())
            {
                string connectionString = _context.Database.Connection.ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 120;

                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                            }
                        }

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    string username = reader["AdminName"].ToString();
                                    string email = reader["Admin_Email_Id"].ToString();
                                    int user_id = DBHelper.ConvertStringToInt(reader["Admin_Id"].ToString());

                                    // Create an object to hold the values
                                    //var user = new { Username = username, Email = email, User_Id = user_id };

                                    // Add the object to a list or process it as needed
                                    // In this example, I'm simply returning the first user found
                                    _adminModel.Admin_Id = user_id;
                                    _adminModel.AdminName = username;
                                    _adminModel.Admin_Email_Id = email;

                                    // If you want to return multiple users, you can store them in a list
                                    // and return the list after the loop
                                    // userList.Add(user);
                                }
                            }
                        }
                    }
                }
            }

            return _adminModel;
        }

        public static QuizModel GenerateQuiz(string commandText , Dictionary<string, object> parameters)
        {
            QuizModel _quizModel = new QuizModel();
            QuizComputation_452Entities _context = new QuizComputation_452Entities();
            string connectionString = _context.Database.Connection.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(commandText , connection)) 
                {

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 120;

                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                        }
                    }

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string title = reader["Title"].ToString();
                                string description = reader["Description"].ToString();


                                _quizModel.Title = title;
                                _quizModel.Description = description;
                            }
                        }

                    }

                }
            }
            return _quizModel;
        }

        public static List<QuizModel> GetQuizList(string commandText, Dictionary<string, object> parameters)
        {
            List<QuizModel> quizList = new List<QuizModel>();
            QuizComputation_452Entities _context = new QuizComputation_452Entities();
            string connectionString = _context.Database.Connection.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 120;

                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                        }
                    }

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                QuizModel quiz = new QuizModel();

                                quiz.Quiz_id = DBHelper.ConvertStringToInt(reader["Quiz_id"].ToString());
                                quiz.Title = reader["Title"].ToString();
                                quiz.Description = reader["Description"].ToString();

                                quizList.Add(quiz);
                            }
                        }
                    }
                }
            }
            return quizList;
        }

        public static void AddQuestionWithOptions(QuestionModel question)
        {
            QuizComputation_452Entities _context = new QuizComputation_452Entities();
            string connectionString = _context.Database.Connection.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SP_AddQuestionWithOptions", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@QuestionText", question.Question_txt);

                DataTable optionsTable = new DataTable();
                optionsTable.Columns.Add("Option_Text", typeof(string));
                optionsTable.Columns.Add("IsCorrect", typeof(bool));

                foreach (var option in question.Options)
                {
                    optionsTable.Rows.Add(option.Option_Text, option.IsCorrect);
                }

                SqlParameter optionsParam = command.Parameters.AddWithValue("@Options", optionsTable);
                optionsParam.SqlDbType = SqlDbType.Structured;
                optionsParam.TypeName = "dbo.OptionTableType";

                int questionId = (int)command.ExecuteScalar();
                // Do something with the questionId if needed
            }
        }




    }
}
