using QuizComputation.Model.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizComputation.Repository.Interface
{
    public interface IAdminInterface
    {
        AdminModel GetAdminProfile(int AdminId);

        int UpdateAdminProfile(AdminModel adminModel);
    }
}
