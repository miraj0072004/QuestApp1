using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuestionRevisedApi.Models;

namespace QuestionRevisedApi.Data
{
    public class MockQuestionRepository : IQuestionsRepository
    {
        private List<Question> _questions;

        public MockQuestionRepository()
        {
            _questions = new List<Question>
            {
                new Question
                {
                    Id = 1,
                    QuestionText = "How many letters are there in the English alphabet ?",
                    AnswerOne = "23",
                    AnswerTwo = "26",
                    AnswerThree = "24",
                    AnswerExplanation = "Count the letters the in the freaking English alphabet you loon",
                    CorrectAnswerIndex = 2

                },
                new Question
                {
                    Id = 2,
                    QuestionText = "Who is the father of Evolution ?",
                    AnswerOne = "Isaac Newton",
                    AnswerTwo = "Tom Cruise",
                    AnswerThree = "Charles Darwin",
                    AnswerExplanation = "Charles Darwin is the founder of the Theory of Evolution",
                    CorrectAnswerIndex = 3

                },
                new Question
                {
                    Id = 3,
                    QuestionText = "How old was Jesus when he was crucified?",
                    AnswerOne = "23",
                    AnswerTwo = "36",
                    AnswerThree = "33",
                    AnswerExplanation = "He was crucified in A.D 33",
                    CorrectAnswerIndex = 3

                }
            };



        }
        public List<Question> GetQuestions()
        {
            return _questions;
        }

        public Question GetQuestion(int id)
        {
            return _questions.Find(x => x.Id == id);
        }
    }
}
