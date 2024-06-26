﻿using QuizComputation.Model.CustomModel;
using QuizComputation.Model.DBContext;
using QuizComputation_Helper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace QuizComputation.Model.GenericRepository
{
    public class GenericRepository
    {
        /// <summary>
        /// Check Sign Up details Correct and signup .
        /// </summary>
        public static bool IsSignUpValid(string commandText, Dictionary<string, object> parameters)
        {
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

        /// <summary>
        /// Check Email already exist .
        /// </summary>

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
        /// <summary>
        /// Check User Details and already exist or not . 
        /// </summary>

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

        /// <summary>
        /// Check Admin Details and already exist or not . 
        /// </summary>
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

        /// <summary>
        /// Create New Quiz 
        /// </summary>
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

        /// <summary>
        /// Get QuizList
        /// </summary>
        /// <returns>Created All quiz List</returns>
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

        /// <summary>
        /// Add Question and option into Quiz
        /// </summary>
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

        /// <summary>
        /// Get Admin Profile details
        /// </summary>
        /// <returns>Admin Profile Info.</returns>
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

        /// <summary>
        /// Update Admin profile after changes.
        /// </summary>
        /// <returns>1 or 0</returns>
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

        /// <summary>
        /// For Edit Quiz Get Quiz Details with options
        /// </summary>
        /// <returns>Quiz , question and options</returns>
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
                                Question_id = (int)reader["Question_id"],
                                Quiz_id = (int)reader["Quiz_id"],
                                Question_txt = reader["Question_txt"].ToString(),
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

        /// <summary>
        /// Update Quiz into DB After changes
        /// </summary>
        /// <param name="quizModel"></param>
        public static void UpdateQuiz(CustomQuizModel quizModel)
        {
            QuizComputation_452Entities _context = new QuizComputation_452Entities();
            string connectionString = _context.Database.Connection.ConnectionString; 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Update Quiz
                //using (SqlCommand command = new SqlCommand("UpdateQuiz", connection))
                //{
                //    command.CommandType = CommandType.StoredProcedure;
                //    command.Parameters.AddWithValue("@QuizId", quizModel.Quiz_id);  
                //    command.Parameters.AddWithValue("@Title", quizModel.Title);  
                //    command.Parameters.AddWithValue("@Description", quizModel.Description);

                //    command.ExecuteNonQuery();
                //}
                
                // Update Questions and Options
                foreach (var question in quizModel.Questions)
                {
                    using (var cmd = new SqlCommand("UpdateQuestion", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@QuestionId", question.Question_id);
                        cmd.Parameters.AddWithValue("@QuestionTxt", question.Question_txt);

                        cmd.ExecuteNonQuery();
                    }

                    // Update each Option for the current Question
                    foreach (var option in question.Options)
                    {
                        using (var cmd = new SqlCommand("UpdateOption", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@questionId", option.Question_id);
                            cmd.Parameters.AddWithValue("@OptionId", option.option_id);
                            cmd.Parameters.AddWithValue("@OptionText", option.Option_text);
                            cmd.Parameters.AddWithValue("@IsCorrect", option.Is_correct);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// User Side show Created All Quiz List
        /// </summary>
        /// <returns></returns>
        public static List<QuizModel> GetQuizListForUser(string commandText, Dictionary<string, object> parameters)
        {

            List<QuizModel> result = new List<QuizModel>();
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
                            // Check if there are any rows returned
                            if (reader.HasRows)
                            {
                                // Read each row
                                while (reader.Read())
                                {
                                    QuizModel QuizzeModel = new QuizModel();

                                    int quiz_id = DBHelper.ConvertStringToInt(reader["Quiz_id"].ToString());
                                    string title = reader["Title"].ToString();
                                    string description = reader["Description"].ToString();
                                    QuizzeModel.Quiz_id = quiz_id;
                                    QuizzeModel.Title = title;
                                    QuizzeModel.Description = description;
                                    result.Add(QuizzeModel);
                                }
                            }
                        }

                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Get Question From Question ID 
        /// </summary>
        /// <returns>Particular Question Text : string</returns>
        public static string GetQuestionByQuestionID(string commandText, Dictionary<string, object> parameters)
        {

            string question_text = "";
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
                            // Check if there are any rows returned
                            if (reader.HasRows)
                            {
                                // Read each row
                                while (reader.Read())
                                {
                                    QuestionModel QuestionModel = new QuestionModel();

                                    question_text = reader["Question_txt"].ToString();
                                    QuestionModel.Question_txt = question_text;
                                    
                                }
                            }
                        }

                    }
                }
            }

            return question_text;
        }        
        
        /// <summary>
        /// Get Question ID
        /// </summary>
        /// <returns>Question Top 1 ID</returns>
        public static int GetQuestionId(string commandText, Dictionary<string, object> parameters)
        {

            int questionID = 0;
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
                            // Check if there are any rows returned
                            if (reader.HasRows)
                            {
                                // Read each row
                                while (reader.Read())
                                {
                                    QuestionModel QuestionModel = new QuestionModel();

                                    questionID = DBHelper.ConvertStringToInt(reader["Question_id"].ToString());
                                    
                                }
                            }
                        }

                    }
                }
            }

            return questionID;
        }

        /// <summary>
        /// Get Option By Question ID
        /// </summary>
        /// <returns>Options</returns>
        public static List<OptionModel> GetOptionByQuestionId(string commandText, Dictionary<string, object> parameters)
        {

            List<OptionModel> result = new List<OptionModel>();
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
                            // Check if there are any rows returned
                            if (reader.HasRows)
                            {
                                // Read each row
                                while (reader.Read())
                                {
                                    OptionModel OptionModel = new OptionModel();

                                    int question_id = DBHelper.ConvertStringToInt(reader["Question_id"].ToString());
                                    int option_id = DBHelper.ConvertStringToInt(reader["Option_id"].ToString());
                                    string option_text = reader["Option_text"].ToString();
                                    OptionModel.Question_id = question_id;
                                    OptionModel.Option_id = option_id;
                                    OptionModel.Option_text = option_text;
                                    result.Add(OptionModel);
                                }
                            }
                        }

                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Save Quiz Answer of user In DB
        /// </summary>
        /// <returns>Row affected</returns>
        public static int SaveUserAnswer(string commandText, Dictionary<string, object> parameters)
        {
            int row_Affected = 0;
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

                    row_Affected = command.ExecuteNonQuery();

                }
            }
            return row_Affected;
        }

        /// <summary>
        /// Calculate Quiz result 
        /// </summary>
        /// <returns>Total Marks</returns>
        public static int CalculateUserQuizResult(string commandText, Dictionary<string, object> parameters)
        {
            int Total_Correct_Answer  = 0;
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
                        // Check if there are any rows returned
                        if (reader.HasRows)
                        {
                            // Read each row
                            while (reader.Read())
                            {
                                Total_Correct_Answer = DBHelper.ConvertStringToInt(reader["correctAnswer"].ToString());
                            }
                        }
                    }

                }
            }
            return Total_Correct_Answer;
        }

        /// <summary>
        /// Delete Quiz From Admin Side
        /// </summary>
        public static void DeleteQuizFromDB(string storedProcedureName, Dictionary<string, object> parameters)
        {
            QuizComputation_452Entities _context = new QuizComputation_452Entities();
            string connectionString = _context.Database.Connection.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                    connection.Open();
                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    foreach (var parameter in parameters)
                    {
                        command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }

                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Get Admin Profile details
        /// </summary>
        /// <returns>Admin Profile Info.</returns>
        public static UserModel GetUserProfile(string commandText, Dictionary<string, object> parameters)
        {
            UserModel _UserModel = new UserModel();
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
                                int UserID = DBHelper.ConvertStringToInt(reader["User_id"].ToString());
                                string Username = reader["username"].ToString();
                                string UserEmail = reader["Email"].ToString();
                                string UserPassword = reader["Password_hash"].ToString();

                                _UserModel.User_id = UserID;
                                _UserModel.Username = Username;
                                _UserModel.Email = UserEmail;
                                _UserModel.Password_hash = UserPassword;
                            }
                        }

                    }

                }
            }
            return _UserModel;
        }

        /// <summary>
        /// Update User profile after changes.
        /// </summary>
        /// <returns>1 or 0</returns>
        public static int UpdateUserProfile(string commandText, Dictionary<string, object> parameters)
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

    }
}
