using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizComputation.Model.CustomModel
{
    public class UserModel
    {
        public int User_id { get; set; }

        [Required(ErrorMessage ="Username should not be empty.")]
        [MinLength(5,ErrorMessage ="Minimum length should be 5.")]
        [MaxLength(8,ErrorMessage ="Maximum length should be 8.")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)[a-zA-Z\d]+$" , ErrorMessage ="Username must contain alphabest and numbers.")]
        public string Username { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email id.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password should not be empty.")]
        public string Password_hash { get; set; }

        [Required(ErrorMessage = "Confirm Password Required.")]
        [Compare("Password_hash", ErrorMessage = "Password is not identical")]
        public string ConfirmPassword { get; set; }

        public Nullable<System.DateTime> Created_at { get; set; }
        public Nullable<System.DateTime> Updated_at { get; set; }
    }

    public class AdminModel
    {
        public int Admin_Id { get; set; }

        [Required(ErrorMessage ="Name must be required")]
        [MinLength(5, ErrorMessage = "Minimum length should be 5.")]
        [MaxLength(8, ErrorMessage = "Maximum length should be 8.")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)[a-zA-Z\d]+$", ErrorMessage = "Username must contain alphabest and numbers.")]
        public string AdminName { get; set; }

        [Required(ErrorMessage = "Email Id must be required")]
        [RegularExpression(@"^[a-zA-Z0-9_.%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email id.")]
        public string Admin_Email_Id { get; set; }

        [Required(ErrorMessage = "Password should not be empty.")]
        [MinLength(5, ErrorMessage = "Minimum length should be 5.")]
        [MaxLength(10, ErrorMessage = "Maximum length should be 10.")]
        public string Admin_password { get; set; }

        [Required(ErrorMessage = "Confirm Password Required.")]
        [Compare("Admin_password", ErrorMessage = "Password is not identical")]
        public string Confirm_password { get; set; }

    }
}
