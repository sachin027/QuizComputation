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
}
