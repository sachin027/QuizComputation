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
        List<QuizModel> GetQuizListForUser(QuizModel QuizModel);

        string GetQuestionByQuizID(int QuizID);

        int GetQuestionId(int QuizId);
        List<OptionModel> GetOptionByQuestionId(int QuestionID);
    }
}
