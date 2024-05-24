using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizComputation.SessionHelper
{
    public class SessionHelper
    {
        public static int user_id
        {
            set
            {
                HttpContext.Current.Session["user_id"] = value;
            }
            get
            {
                return HttpContext.Current.Session["user_id"] == null ? 0 : (int)HttpContext.Current.Session["user_id"];
            }
        }
        public static string username
        {
            get
            {
                return HttpContext.Current.Session["username"] == null ? "" : (string)HttpContext.Current.Session["username"];
            }
            set
            {
                HttpContext.Current.Session["username"] = value;
            }
        }

        public static string email
        {
            get
            {
                return HttpContext.Current.Session["email"] == null ? "" : (string)HttpContext.Current.Session["email"];
            }
            set
            {
                HttpContext.Current.Session["email"] = value;
            }
        }
    }
}