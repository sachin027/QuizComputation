using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizComputation.Model.CustomModel
{
    public class UserAnswerModel
    {
        public int Answer_id { get; set; }
        public Nullable<int> User_id { get; set; }
        public Nullable<int> Quiz_id { get; set; }
        public Nullable<int> Question_id { get; set; }
        public Nullable<int> Selected_Option_id { get; set; }
        public Nullable<System.DateTime> Created_at { get; set; }
    }
}
