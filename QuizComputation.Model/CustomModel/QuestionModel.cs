using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizComputation.Model.CustomModel
{
    public class QuestionModel
    {
        public int Question_id { get; set; }
        public Nullable<int> Quiz_id { get; set; }

        public string Question_txt { get; set; }

        public List<OptionViewModel> Options { get; set; }

        public QuestionModel()
        {
            Options = new List<OptionViewModel>();
        }

        public Nullable<System.DateTime> Created_at { get; set; }
        public Nullable<System.DateTime> Updated_at { get; set; }
    }

    public class OptionViewModel
    {
        public int Option_Id { get; set; }
        public Nullable<int> Question_id { get; set; }
        public string Option_Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
