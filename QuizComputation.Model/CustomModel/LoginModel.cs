﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizComputation.Model.CustomModel
{
    public class LoginModel
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email id.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password should not be empty.")]
        public string Password_hash { get; set; }


    }
}
