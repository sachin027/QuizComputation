using QuizComputation.Common;
using QuizComputation.CustomFilter;
using QuizComputation.Model.CustomModel;
using QuizComputation.Model.GenericRepository;
using QuizComputation.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace QuizComputation.Controllers
{
    [CustomAuthorize]
    [CustomUserAuthorize]
    public class UserController : Controller
    {
        UserService _userService = new UserService();
        // GET: User
        public async Task<ActionResult> UserDashboard(QuizModel quizModel) 
        {
            ViewBag.UserId = SessionHelper.SessionHelper.user_id;
            ViewBag.userName = SessionHelper.SessionHelper.username;
            List<QuizModel> _quizList = await WebAPIHelper.GetQuizListForUser();
            //_userService.GetQuizListForUser();
            return View(_quizList);
        }

        public ActionResult UserProfile(int UserID)
        {
            //UserID = SessionHelper.SessionHelper.user_id;

            UserModel _userModel = _userService.GetUserProfile(UserID);
            return View(_userModel);
        }

        [HttpPost]
        public ActionResult UserProfile(UserModel _UserModel)
        {
                int isRowAffected = _userService.UpdateUserProfile(_UserModel);
                if (isRowAffected > 0)
                {
                    return View("UserDashboard");
                }
                else
                {
                    return View();
                }

        }
        public ActionResult ShowInformationPage(int QuizID)
        {
            try
            {
                ViewBag.QuizID = QuizID;
                QuizModel quizModel = new QuizModel();
                int Userid = SessionHelper.SessionHelper.user_id;
                ViewBag.UserId = Userid;
                ViewBag.QuestionId = _userService.GetQuestionId(QuizID);
                return View(quizModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult QuizQuestionForUser(int QuestionId)
        {
            
            ViewBag.Questions = _userService.GetQuestionByQuestionID(QuestionId);
            List<OptionModel> OptionModel = _userService.GetOptionByQuestionId(QuestionId);
            return PartialView("QuizQuestionForUser", OptionModel);
        }

        public JsonResult AddUserAnswers(UserAnswerModel _UserAnswer)
        {
            int isAnswers = _userService.SaveUserAnswer(_UserAnswer);
            return Json(isAnswers, JsonRequestBehavior.AllowGet);
        }

        public ActionResult QuizResult(int QuizID , int UserId)
        {
            ViewBag.UserID = SessionHelper.SessionHelper.user_id;
            ViewBag.QuizId = QuizID; 
            int QuizTotalMarks = _userService.UserQuizResult(QuizID , UserId);
            ViewBag.QuizTotalMarks = QuizTotalMarks;
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            TempData["success"] = "Logout successfully ";
            return RedirectToAction("Login", "Login");
        }
    }
}