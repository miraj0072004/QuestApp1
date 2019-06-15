using System;
using System.Collections.Generic;
using System.Text;

namespace QuestApp1.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string AnswerDescription { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
