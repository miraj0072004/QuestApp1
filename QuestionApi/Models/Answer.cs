using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestionApi.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionAnswerId { get; set; }
        public string AnswerText { get; set; }
        public bool Correctness { get; set; }

        public int QuestionId { get; set; }
    }
}