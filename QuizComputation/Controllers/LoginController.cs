using QuizComputation.Model.CustomModel;
using QuizComputation.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizComputation.Controllers
{
    public class LoginController : Controller
    {
        LoginService _loginService = new LoginService();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel _UserLogin)
        {
            if (ModelState.IsValid)
            {
                UserModel _user = _loginService.UserLogin(_UserLogin);
                AdminModel _admin = _loginService.AdminLogin(_UserLogin);

                if (_user.User_id > 0 && _user.Email!=null && _admin.Admin_Email_Id == null)
                {
                    SessionHelper.SessionHelper.user_id = _user.User_id;
                    SessionHelper.SessionHelper.username = _user.Username;
                    SessionHelper.SessionHelper.email = _user.Email;
                    SessionHelper.SessionHelper.Role = _user.Role;

                    TempData["success"] = "Login Successfully";
                    return RedirectToAction("UserDashboard","User");
                }
                if (_admin.Admin_Id > 0 && _admin != null)
                {
                    SessionHelper.SessionHelper.user_id = _admin.Admin_Id;
                    SessionHelper.SessionHelper.username = _admin.AdminName;
                    SessionHelper.SessionHelper.email = _admin.Admin_Email_Id;
                    SessionHelper.SessionHelper.Role = _admin.Role;

                    ViewBag.CreatedByValue = SessionHelper.SessionHelper.user_id;

                    TempData["success"] = "Login Successfully";
                    return RedirectToAction("AdminDashboard", "Admin");
                }
                else
                {
                    TempData["error"] = "User not exist";
                    return View();
                }
            }
            else
            {
                TempData["error"] = "Check Credentials";
                return View();
            }
        }

        // GET: Signup
        public ActionResult Signup()
        {
            return View("Signup");
        }

        [HttpPost]
        public ActionResult Signup(UserModel _UserModel)
        {
            if (ModelState.IsValid)
            {
                bool IsEmailExist = _loginService.CheckUserAlreadyExist(_UserModel);
                if (IsEmailExist)
                {
                    
                    bool IsUserAdded = _loginService.AddUserIntoDatabase(_UserModel);
                    if (IsUserAdded)
                    {
                        TempData["success"] = "Registration Successfully";
                        return View("Login");
                    }
                    else
                    {
                        TempData["error"] = "Something went wrong";
                        return View();
                    }

                }
                else
                {
                    ModelState.AddModelError("Email", "Email I'd already exist");
                    TempData["error"] = "Something went wrong";
                    return View();
                }
            }
            else
            {
                TempData["error"] = "Something went wrong";
                return View();
            }
        }


    }
}