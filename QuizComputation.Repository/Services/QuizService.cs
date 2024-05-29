using QuizComputation.Model.CustomModel;
using QuizComputation.Model.GenericRepository;
using QuizComputation.Repository.Interface;
using QuizComputation_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizComputation.Repository.Services
{
    public class QuizService : IQuizInterface
    {
        /// <summary>
        /// Create new quiz
        /// </summary>
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

        /// <summary>
        /// Get created Quiz List
        /// </summary>
        public List<QuizModel> GetCreatedQuizList( )
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

        /// <summary>
        /// Add Question With option In quiz
        /// </summary>
        public void AddQuestion(List<OptionQuestionModel> _QustionAddingModel)
        {
            string AddQuestionsAndOptions = "SP_AddQuestionsWithOptions";
            foreach (var item in _QustionAddingModel)
            {
                OptionQuestionModel _QustionAddingModels = new OptionQuestionModel();
                string Answers = item.Answers;
                string options1 = item.options1;
                string options2 = item.options2;
                string options3 = item.options3;
                string options4 = item.options4;
                string question = item.question;
                int quizId = item.quizId;

                //int quizId_ = item.quizId;
                Dictionary<string, object> Parameters = new Dictionary<string, object>
                {
                    {"@quiz_id" ,quizId},
                    {"@question_text ",question},
                    {"@options1 ", options1},
                    {"@options2 ",options2},
                    {"@options3", options3},
                    {"@options4", options4},
                    { "@Answers",Answers}
                };
                GenericRepository.AddQuestionWithOption(AddQuestionsAndOptions, Parameters);
            }

        }

        /// <summary>
        /// Delete Quiz From Admin Side
        /// </summary>
        public void DeleteQuizFromDB(int QuizId)
        {
            string SP_DeleteQuiz = "SP_DeleteQuiz";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"@QuizId" ,QuizId},
            };

            GenericRepository.DeleteQuizFromDB("SP_DeleteQuiz", parameters);
        }
    }
}
