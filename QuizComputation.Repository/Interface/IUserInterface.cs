using QuizComputation.Model.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizComputation.Repository.Interface
{
    public interface IUserInterface
    {
        List<QuizModel> GetQuizListForUser();

        string GetQuestionByQuestionID(int QuizID);

        int GetQuestionId(int QuizId);
        List<OptionModel> GetOptionByQuestionId(int QuestionID);

        int SaveUserAnswer(UserAnswerModel userAnswerModel);

        int UserQuizResult(int QuizID, int UserID);

        UserModel GetUserProfile(int UserId);

        int UpdateUserProfile(UserModel userModel);
    }
}
