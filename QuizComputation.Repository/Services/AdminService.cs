using QuizComputation.Model.CustomModel;
using QuizComputation.Model.GenericRepository;
using QuizComputation.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuizComputation.Repository.Services
{
    public class AdminService : IAdminInterface
    {
        public AdminModel GetAdminProfile(int AdminId)
        {
            try
            {

                string SP_GetAdminProfile = "SP_GetAdminProfile";

                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@adminID" , AdminId}
                };

                AdminModel _admin = GenericRepository.GetAdminProfile(SP_GetAdminProfile , parameters);
                return _admin;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateAdminProfile(AdminModel adminModel)
        {
            try
            {
                int AdminId = adminModel.Admin_Id;
                string AdminName = adminModel.AdminName;
                string AdminEmail = adminModel.Admin_Email_Id;
                string AdminPassword = HashPassword( adminModel.Admin_password);
                adminModel.Admin_password = AdminPassword;

                string SP_UpdateAdminProfile = "SP_UpdateAdminProfile";

                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                            { "@AdminId" , AdminId},
                            { "@password", AdminPassword},
                            { "@email", AdminEmail },
                            { "@username", AdminName }
                };

                int IsRowAffected = GenericRepository.UpdateAdminProfile(SP_UpdateAdminProfile,parameters);
                if(IsRowAffected > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
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
