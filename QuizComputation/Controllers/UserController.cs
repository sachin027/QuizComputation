using QuizComputation.Model.CustomModel;
using QuizComputation.Model.GenericRepository;
using QuizComputation.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizComputation.Controllers
{
    public class UserController : Controller
    {
        UserService _userService = new UserService();
        // GET: User
        public ActionResult UserDashboard(QuizModel quizModel) 
        {
            List<QuizModel> _quizList = _userService.GetQuizListForUser(quizModel);
            return View(_quizList);
        }

        public ActionResult ShowInformationPage(int QuizID)
        {
            try
            {
                ViewBag.quizID = QuizID;
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult StartQuiz(int id)
        {
            ViewBag.QuestionId = _userService.GetQuestionId(id);
            int questionID = ViewBag.QuestionId;
            ViewBag.Questions = _userService.GetQuestionByQuizID(id);
            List<OptionModel> optionList = _userService.GetOptionByQuestionId(questionID);
            return PartialView("_PartialQuestion", optionList);
        }
    }
}