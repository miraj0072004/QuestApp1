using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestionApi.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string AnswerDescription { get; set; }
        public List<Answer> Answers { get; set; }

        public string UserId { get; set; }
    }
}