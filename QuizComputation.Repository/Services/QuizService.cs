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
    public class QuizService : IQuizInterface
    {
        public QuizModel CreateQuiz(QuizModel _quizModel)
        {
            try
            {
                string title = _quizModel.Title;
                string description = _quizModel.Description;
                string SP_CreateQuiz = "SP_CreateQuiz";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@Title" , title},
                    { "@Description" , description}
                };

                QuizModel _quiz = GenericRepository.GenerateQuiz(SP_CreateQuiz, parameters);
                return _quiz;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<QuizModel> GetCreatedQuizList(QuizModel _quizModel)
        {
            try
            {
                string SP_GetCreatedQuizList = "SP_GetCreatedQuizList";
                Dictionary<string, object> parameters = null;

                List<QuizModel> _quiz = GenericRepository.GetQuizList(SP_GetCreatedQuizList, parameters);
                return _quiz;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddQuestionWithOptions(QuestionModel question)
        {
            try
            {
                GenericRepository.AddQuestionWithOptions(question);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
