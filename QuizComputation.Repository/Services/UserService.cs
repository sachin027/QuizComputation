using QuizComputation.Model.CustomModel;
using QuizComputation.Model.GenericRepository;
using QuizComputation.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizComputation.Repository.Services
{
    public class UserService : IUserInterface
    {
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

        public string GetQuestionByQuizID(int QuizID)
        {
            string Question;
            string GetQuestionByQuizID = "SP_GetQuestionByQuizId";

            Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@QuizId" , QuizID}
                };

            Question = GenericRepository.GetQuestionByQuizId(GetQuestionByQuizID, parameters);
            return Question;
        }

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

        public List<QuizModel> GetQuizListForUser(QuizModel QuizModel)
        {
            List<QuizModel> QuizzeModelList = new List<QuizModel>();
            string QuizListForUser = "GetQuizListForUser";
            QuizzeModelList = GenericRepository.GetQuizListForUser(QuizListForUser, null);
            return QuizzeModelList;
        }
    }
}
