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

        public static void AddQuestionWithOption(string commandText, Dictionary<string, object> parameters)
        {
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
                        command.ExecuteNonQuery();


                    }
                }
            }
        }


        public static AdminModel GetAdminProfile(string commandText, Dictionary<string, object> parameters)
        {
            AdminModel _adminModel = new AdminModel();
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
                                int adminID = DBHelper.ConvertStringToInt( reader["Admin_Id"].ToString());
                                string adminName = reader["AdminName"].ToString();
                                string adminEmail= reader["Admin_Email_Id"].ToString();
                                string adminPassword = reader["Admin_password"].ToString();

                                _adminModel.Admin_Id = adminID;
                                _adminModel.AdminName = adminName;
                                _adminModel.Admin_Email_Id = adminEmail;
                                _adminModel.Admin_password = adminPassword;
                            }
                        }

                    }

                }
            }
            return _adminModel;
        }

        public static int UpdateAdminProfile(string commandText, Dictionary<string, object> parameters)
        {
            int IsRowAffected = 0;
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

                    IsRowAffected = command.ExecuteNonQuery();

                }
            }
            return IsRowAffected;
        }

        public static CustomQuizModel GetQuizWithQuestionsAndOptions(int quizId)
        {
            var quizModelList = new CustomQuizModel();
            QuizComputation_452Entities _context = new QuizComputation_452Entities();
            string connectionString = _context.Database.Connection.ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Get Quiz
                using (var cmd = new SqlCommand("GetQuizFromQuizId", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@QuizId", quizId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            quizModelList.Quiz_id = (int)reader["Quiz_id"];
                            quizModelList.Title = reader["Title"].ToString();
                            quizModelList.Description = reader["Description"].ToString();
                        }
                    }
                }

                // Get Questions
                quizModelList.Questions = new List<CustomQuestionModel>();
                using (var cmd = new SqlCommand("GetQuestionsByQuizId", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@QuizId", quizId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var question = new CustomQuestionModel
                            {
                                Question_id = (int)reader["question_id"],
                                Quiz_id = (int)reader["quiz_id"],
                                Question_txt = reader["question_text"].ToString(),
                                Options = new List<CustomOptionModel>()
                            };

                            quizModelList.Questions.Add(question);
                        }
                    }
                }

                // Get Options
                foreach (var question in quizModelList.Questions)
                {
                    using (var cmd = new SqlCommand("GetOptionsByQuestionId", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@QuestionId", question.Question_id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var option = new CustomOptionModel
                                {
                                    option_id = (int)reader["option_id"],
                                    Question_id = (int)reader["Question_id"],
                                    Option_text = reader["Option_text"].ToString(),
                                    Is_correct = (bool)reader["Is_correct"]
                                };

                                question.Options.Add(option);
                            }
                        }
                    }
                }
            }

            return quizModelList;
        }
    }
}
