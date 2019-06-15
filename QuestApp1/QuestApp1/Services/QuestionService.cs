using System;
using System.Collections.Generic;
using System.Text;
using QuestApp1.Models;
using Xamarin.Forms;

namespace QuestApp1.Services
{
    class QuestionService
    {
        public Question GetQuestionById(int id)
            => new Question()
            {
                Id = 1,
                QuestionText = "What is 2 + 2 ?",
                Answers = new List<Answer>()
                {
                    new Answer()
                    {
                        Id = 1,
                        AnswerText = "3",
                        Correctness = false
                    },
                    new Answer()
                    {
                        Id = 1,
                        AnswerText = "4",
                        Correctness = true
                    },
                    new Answer()
                    {
                        Id = 1,
                        AnswerText = "5",
                        Correctness = false
                    }
                },
                AnswerDescription = "This is definitional you idiot!"
               
            };

    }
}
