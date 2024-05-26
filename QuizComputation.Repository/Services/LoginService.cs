
using QuizComputation.Model.GenericRepository;
using QuizComputation.Model.CustomModel;
using QuizComputation.Model.DBContext;
using QuizComputation.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace QuizComputation.Repository.Services
{
    public class LoginService : ILoginInterface
    {
        public bool AddUserIntoDatabase(UserModel _UserModel)
        {
            try
            {
                string password_hash = HashPassword(_UserModel.Password_hash);
                _UserModel.Password_hash = password_hash;

                string username = _UserModel.Username;
                string email = _UserModel.Email;
                string AddUser = "AddUser";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                            { "@username", username},
                            { "@password_hash", password_hash },
                            { "@email", email }
                };
                bool IsSignUpValid = GenericRepository.IsSignUpValid(AddUser, parameters);
                return IsSignUpValid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AdminModel AdminLogin(LoginModel _userLogin)
        {
            try
            {
                string Email = _userLogin.Email;
                string password = HashPassword(_userLogin.Password_hash);
                _userLogin.Password_hash = password;
                string IsAdminExist = "SP_LoginAdmin";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                        { "@Email", Email},
                        { "@password", password}
                };
                AdminModel _admin = GenericRepository.IsAdminExist(IsAdminExist, parameters);
                return _admin;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckUserAlreadyExist(UserModel _userModel)
        {
            try
            {
                string Email = _userModel.Email;
                string IsEmailExist = "SP_IsEmailExist";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                        { "@Email", Email}
                };
                bool CheckEmailExist = GenericRepository.IsEmailExist(IsEmailExist, parameters);
                return CheckEmailExist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserModel UserLogin(LoginModel _userLogin)
        {
            try
            {
                string Email = _userLogin.Email;
                string password = HashPassword(_userLogin.Password_hash);
                _userLogin.Password_hash = password;
                string IsUserExist = "SP_LoginUser";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                        { "@Email", Email},
                        { "@password", password}
                };
                UserModel _user = GenericRepository.IsUserExist(IsUserExist, parameters);
                return _user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private string HashPassword(string password)
        {
            using(var sha256 = SHA256.Create())
            {
                if (password != null)
                {
                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    return Convert.ToBase64String(hashedBytes);
                }
                return null;
            }
        }
    }
}
