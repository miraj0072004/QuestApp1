using System;
using System.Collections.Generic;
using System.Text;
using QuestApp1.Models;
using Xamarin.Forms;

namespace QuestApp1.Services
{
    class QuestionService
    {
        private List<Question> Questions = new List<Question> {
                new Question()
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
                            Id = 2,
                            AnswerText = "4",
                            Correctness = true
                        },
                        new Answer()
                        {
                            Id = 3,
                            AnswerText = "5",
                            Correctness = false
                        }
                    },
                    AnswerDescription = "This is definitional you idiot!"

                },
                new Question()
                {
                    Id = 2,
                    QuestionText = "Who is the father of modern philosophy ?",
                    Answers = new List<Answer>()
                    {
                        new Answer()
                        {
                            Id = 1,
                            AnswerText = "René Descartes",
                            Correctness = true
                        },
                        new Answer()
                        {
                            Id = 2,
                            AnswerText = "Aristotle",
                            Correctness = false
                        },
                        new Answer()
                        {
                            Id = 3,
                            AnswerText = "David Hume",
                            Correctness = false
                        }
                    },
                    AnswerDescription = "René Descartes is generally considered the father of modern philosophy. He was the first major figure in the philosophical movement known as rationalism, a method of understanding the world based on the use of reason as the means to attain knowledge.!"

                }
            };

        public Question GetQuestionById(int id)
            //=> new Question()
            //{
            //    Id = 1,
            //    QuestionText = "What is 2 + 2 ?",
            //    Answers = new List<Answer>()
            //    {
            //        new Answer()
            //        {
            //            Id = 1,
            //            AnswerText = "3",
            //            Correctness = false
            //        },
            //        new Answer()
            //        {
            //            Id = 1,
            //            AnswerText = "4",
            //            Correctness = true
            //        },
            //        new Answer()
            //        {
            //            Id = 1,
            //            AnswerText = "5",
            //            Correctness = false
            //        }
            //    },
            //    AnswerDescription = "This is definitional you idiot!"

            //};
            => this.Questions.Find(q => q.Id == id);

    }
}
