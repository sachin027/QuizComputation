using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizComputation.Model.CustomModel
{
    public class QuizModel
    {
        public int Quiz_id { get; set; }
        [Required(ErrorMessage ="PLease Enter Title*")]
        [StringLength(100 , ErrorMessage ="Title length more than 100 characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please Enter Description*")]
        [StringLength(250, ErrorMessage = "Description length more than 250 characters")]
        public string Description { get; set; }
        public Nullable<int> Created_by { get; set; }
        public Nullable<System.DateTime> Created_at { get; set; }
        public Nullable<System.DateTime> Updated_at { get; set; }
    }

    public class CustomQuizModel
    {
        public int Quiz_id { get; set; }    
        public string Title { get; set; }
        public string Description { get; set; }
        public List<CustomQuestionModel> Questions { get; set; }

    }
    public class CustomQuestionModel
    {
        public int Question_id { get; set; }
        public int Quiz_id { get; set; }
        public string Question_txt { get; set; }
        public List<CustomOptionModel> Options { get; set; }
    }
    public class CustomOptionModel
    {
        public int option_id { get; set; }
        public int Question_id { get; set; }
        public string Option_text { get; set; }
        public bool Is_correct { get; set; }
    }
}
