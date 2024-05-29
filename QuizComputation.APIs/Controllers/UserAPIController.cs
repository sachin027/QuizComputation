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
    public class UserAPIController : ApiController
    {

        private readonly IQuizInterface _QuizInterface;
        private readonly IUserInterface _UserInterface;

        public UserAPIController()
        {
            this._QuizInterface = new QuizService();
            this._UserInterface = new UserService();
        }

        ///<summary>Get All Created Quiz List after craete for Admin</summary>
        [HttpGet]
        [Route("api/UserAPI/GetQuizListForUser")]
        public List<QuizModel> GetQuizListForUser()
        {
            List<QuizModel> _QuizListForUser =  _UserInterface.GetQuizListForUser();
            return _QuizListForUser;
        }

        [HttpPost]
        [Route("api/UserAPI/DeleteQuiz")]
        public void DeleteQuiz(int QuizId)
        {
            _QuizInterface.DeleteQuizFromDB(QuizId);
        }
    }
}