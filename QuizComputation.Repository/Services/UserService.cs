using QuizComputation.Model.CustomModel;
using QuizComputation.Model.GenericRepository;
using QuizComputation.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuizComputation.Repository.Services
{
    public class UserService : IUserInterface
    {
        /// <summary>
        /// Get option by question id
        /// </summary>
        public List<OptionModel> GetOptionByQuestionId(int QuestionID)
        {
            List<OptionModel> OptionList = new List<OptionModel>();
            string GetOptionByQuestionId = "SP_GetOptionsByQuestionID";


            Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@questionId" , QuestionID}
                };
 
            OptionList = GenericRepository.GetOptionByQuestionId(GetOptionByQuestionId, parameters);
            return OptionList;
        }

        /// <summary>
        /// Get question by question id
        /// </summary>
        public string GetQuestionByQuestionID(int QuestionId)
        {
            string Question;
            string GetQuestionByQuizID = "SP_GetQuestionByQuestionId";

            Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@questionID" , QuestionId}
                };

            Question = GenericRepository.GetQuestionByQuestionID(GetQuestionByQuizID, parameters);
            return Question;
        }

        /// <summary>
        /// Get  question id
        /// </summary>
        public int GetQuestionId(int QuizId)
        {
            int questionID;
            string SP_GetQuestionID = "SP_GetQuestionID";

            Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@QuizId" , QuizId}
                };

            questionID = GenericRepository.GetQuestionId(SP_GetQuestionID, parameters);
            return questionID;
        }

        /// <summary>
        /// Get all created Quiz List For user
        /// </summary>
        public List<QuizModel> GetQuizListForUser(QuizModel QuizModel)
        {
            List<QuizModel> QuizzeModelList = new List<QuizModel>();
            string QuizListForUser = "GetQuizListForUser";
            QuizzeModelList = GenericRepository.GetQuizListForUser(QuizListForUser, null);
            return QuizzeModelList;
        }

        /// <summary>
        /// Save user answer in DB
        /// </summary>
        public int SaveUserAnswer(UserAnswerModel userAnswerModel)
        {
            try
            {
                string SP_SaveUserAnswer = "SP_SaveUserAnswer";
                int userId = Convert.ToInt32(userAnswerModel.User_id);
                int questionId = Convert.ToInt32(userAnswerModel.Question_id);
                int quizId = Convert.ToInt32(userAnswerModel.Quiz_id);
                int selectedOptionId = Convert.ToInt32(userAnswerModel.Selected_Option_id);
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                     { "@quizID" , quizId },
                     { "@UserID" , userId },
                     { "@questionID" , questionId },
                     { "@selectOptionID" , selectedOptionId },
                };

                int RowAffected = GenericRepository.SaveUserAnswer(SP_SaveUserAnswer, parameters);

                return RowAffected;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Calculate user quiz result
        /// </summary>
        public int UserQuizResult(int QuizID, int UserID)
        {
            string ShowResult = "SP_ShowQuizResult";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"@UserId", UserID},
                {"@quiz_id",QuizID }
            };

            int ResultOfUser = GenericRepository.CalculateUserQuizResult(ShowResult, parameters);
            int TotalMarks = CalculateAnswers(ResultOfUser);
            return TotalMarks;
        }

        /// <summary>
        /// Calculate Marks
        /// </summary>
        public static int CalculateAnswers(int resultOfUser)
        {
            int Result = resultOfUser * 10;
            return Result;
        }

        /// <summary>
        /// Get User Details
        /// </summary>
        public UserModel GetUserProfile(int UserId)
        {
            try
            {

                string SP_GetUserProfile = "SP_GetUserProfile";

                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@userID" , UserId}
                };

                UserModel _User = GenericRepository.GetUserProfile(SP_GetUserProfile, parameters);
                return _User;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update User Profile
        /// </summary>

        public int UpdateUserProfile(UserModel userModel)
        {
            try
            {
                int UserId = userModel.User_id;
                string UserName = userModel.Username;
                string UserEmail = userModel.Email;
                string UserPassword = HashPassword(userModel.Password_hash);
                userModel.Password_hash = UserPassword;

                string SP_UpdateUserProfile = "SP_UpdateUserProfile";

                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                            { "@userID" , UserId},
                            { "@password",UserPassword },
                            { "@email", UserEmail },
                            { "@username", UserName }
                };

                int IsRowAffected = GenericRepository.UpdateAdminProfile(SP_UpdateUserProfile, parameters);
                if (IsRowAffected > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                if (password != null)
                {
                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    return Convert.ToBase64String(hashedBytes);
                }
                return null;
            }
        }
    }
}
