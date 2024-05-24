using QuizComputation.Model.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizComputation.Repository.Interface
{
    public interface ILoginInterface
    {
        bool CheckUserAlreadyExist(UserModel _user);
        bool AddUserIntoDatabase(UserModel _user);
        UserModel UserLogin(LoginModel _userLogin);
        AdminModel AdminLogin(LoginModel _userLogin);


    }
}
