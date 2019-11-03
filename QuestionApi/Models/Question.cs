using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuestionApi.Models
{
    public class Question
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Question")]
        public string QuestionText { get; set; }
        [Display(Name = "Answer Explanation")]
        public string AnswerDescription { get; set; }

        [Display(Name = "Answers")]
        public List<Answer> Answers { get; set; }

        public string UserId { get; set; }
    }
}