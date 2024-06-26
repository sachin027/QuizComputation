﻿using System;
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
        public int Quiz_id { get; set; }
        public string Question_txt { get; set; }
    }    
    public class OptionModel
    {
        public int Option_id { get; set; }
        public int Question_id { get; set; }
        public string Option_text { get; set; }
        public bool Is_correct { get; set; }
    }
    public class OptionQuestionModel
    {
        public int quizId { get; set; }
        public string question { get; set; }

        public string options1 { get; set; }

        public string options2 { get; set; }

        public string options3 { get; set; }

        public string options4 { get; set; }

        public string Answers { get; set; }

    }


}
