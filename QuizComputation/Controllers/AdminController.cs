using QuizComputation.Model.CustomModel;
using QuizComputation.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizComputation.Controllers
{
    public class AdminController : Controller
    {
        QuizService _quizService = new QuizService();
        // GET: Admin
        public ActionResult AdminDashboard(QuizModel _quizModel)
        {
            List<QuizModel> _QuizList = _quizService.GetCreatedQuizList(_quizModel);
            return View(_QuizList);
        }

        public ActionResult CreateQuiz()
        {
            ViewBag.CreatedByValue = SessionHelper.SessionHelper.user_id;
            return View();
        }
        [HttpPost]
        public ActionResult CreateQuiz(QuizModel _quizModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    QuizModel _quiz = _quizService.CreateQuiz(_quizModel);

                    if(_quizModel != null )
                    {
                        TempData["success"] = "Quiz Created Successfully";
                        return RedirectToAction("AdminDashboard", "Admin");
                    }
                    else
                    {
                        TempData["error"] = "Something Went Wrong";
                        return View();
                    }
                }
                else
                {
                    TempData["error"] = "Please Fill Details";
                    return View();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult AddQuestionIntoQuiz(int QuizID , string description , string title)
        {
            try
            {
                ViewBag.QuizID = QuizID;
                ViewBag.description = description;
                ViewBag.title = title;
                return View();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost]
        public ActionResult AddQuestionIntoQuiz(List<OptionQuestionModel> _QustionAddingModel)
        {
            try
            {
                ViewBag.created_by = SessionHelper.SessionHelper.user_id;
                _quizService.AddQuestion(_QustionAddingModel);
                return View();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}