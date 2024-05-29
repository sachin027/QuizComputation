using QuizComputation.Model.CustomModel;
using QuizComputation.Repository.Interface;
using QuizComputation.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QuizComputation.APIs.Controllers
{
    public class AdminAPIController : ApiController
    {
        private readonly IAdminInterface _AdminInterface;
        private readonly IQuizInterface _QuizInterface;

        public AdminAPIController()
        {
            this._AdminInterface = new AdminService();
            this._QuizInterface = new QuizService();
        }

        ///<summary>Get All Created Quiz List after craete for Admin</summary>
        [HttpGet]
        [Route("api/AdminAPI/GetAllCreatedQuizListForAdmin")]
        public List<QuizModel> GetAllCreatedQuizListForAdmin()
        {
            List<QuizModel> _QuizListForAdmin = new List<QuizModel>();
            _QuizListForAdmin = _QuizInterface.GetCreatedQuizList( );
            return _QuizListForAdmin;
        }

        [HttpPost]
        [Route("api/AdminAPI/CreateQuiz")]
        public QuizModel CreateQuiz(QuizModel quizModel)
        {
            QuizModel _quizModel =  _QuizInterface.CreateQuiz(quizModel);
            return _quizModel;
        }
    }
}