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
    [CustomAdminAuthorize]
    public class AdminController : Controller
    {
        QuizService _quizService = new QuizService();
        AdminService _adminService = new AdminService();

        // GET: Admin
        public async Task<ActionResult> AdminDashboard()
        {
            try
            {
                ViewBag.userName = SessionHelper.SessionHelper.username;
                List<QuizModel> ans = await WebAPIHelper.CreatedQuizListForUser();
                if(ans != null)
                {
                    return View(ans);
                }
                else
                {
                    return View("Login");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            //ViewBag.userName = SessionHelper.SessionHelper.username;
            //List<QuizModel> _QuizList = _quizService.GetCreatedQuizList(_quizModel);
            //return View(_QuizList);
        }



        /// <summary>
        /// Create Quiz Page.
        /// </summary>
        public ActionResult CreateQuiz()
        {
            ViewBag.CreatedByValue = SessionHelper.SessionHelper.user_id;
            return View();
        }

        // POST: Create Quiz
        [HttpPost]
        public async Task<ActionResult> CreateQuiz(QuizModel _quizModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    QuizModel _quiz = await WebAPIHelper.CreateQuiz(_quizModel);
                    //_quizService.CreateQuiz(_quizModel);

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

        /// <summary>
        /// Add Question and option into Quiz
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
                return View("AdminDashboard");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// </summary>
        
        
        /// <summary>
        /// Get Admin Profile for update
        public ActionResult AdminProfile(int id = 1)
        {

            AdminModel _admin = _adminService.GetAdminProfile(id);
            return View(_admin);
        }
        [HttpPost]
        public ActionResult AdminProfile(AdminModel adminModel)
        {
            if (ModelState.IsValid)
            {
                int isRowAffected = _adminService.UpdateAdminProfile(adminModel);
                if(isRowAffected > 0)
                {
                    return View("AdminDashboard");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
        /// </summary>
        
        
        /// <summary>
        /// Edit Quiz
        public ActionResult EditQuiz(int QuizID)
        {
            ViewBag.created_by = SessionHelper.SessionHelper.user_id;
            CustomQuizModel QuizzeModelList = GenericRepository.GetQuizWithQuestionsAndOptions(QuizID);
            return View(QuizzeModelList);
        }

        [HttpPost]
        public ActionResult EditQuiz(CustomQuizModel customQuizModel) 
        {
            try
            {
                GenericRepository.UpdateQuiz(customQuizModel);
                return Json(new { redirectUrl = Url.Action("AdminDashboard") });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// </summary>
        

        /// <summary>
        /// Delete Quiz 
        /// </summary>
        public ActionResult DeleteQuiz(int QuizId)
        {
            _quizService.DeleteQuizFromDB(QuizId);
            return RedirectToAction("AdminDashboard", "Admin");
        }

        /// <summary>
        /// Log Out From Admin 
        /// </summary>
        public ActionResult Logout()
        {
            Session.Clear();
            TempData["success"] = "Logout successfully ";
            return RedirectToAction("Login", "Login");
        }

    }
}