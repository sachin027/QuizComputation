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

        public ActionResult AddQuestionIntoQuiz()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost]
        public ActionResult AddQuestionIntoQuiz(QuestionModel question)
        {
            try
            {
                _quizService.AddQuestionWithOptions(question);
                ViewBag.Message = "Question added successfully";
                return View("AdminDashboard");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}