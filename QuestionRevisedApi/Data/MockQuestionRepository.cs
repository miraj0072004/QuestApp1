using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuestionRevisedApi.Helpers;
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

                },
                 new Question
                {
                    Id = 4,
                    QuestionText = "What is my name?",
                    AnswerOne = "Nadia",
                    AnswerTwo = "Rizna",
                    AnswerThree = "Miraj",
                    AnswerExplanation = "My name is Miraj",
                    CorrectAnswerIndex = 3

                },
                 new Question
                {
                    Id = 5,
                    QuestionText = "What year did the Spanish flu break out?",
                    AnswerOne = "2020",
                    AnswerTwo = "1919",
                    AnswerThree = "1984",
                    AnswerExplanation = "The Spanish flu broke out in 1919",
                    CorrectAnswerIndex = 2

                },
                 new Question
                {
                    Id = 6,
                    QuestionText = "Who is the first female president?",
                    AnswerOne = "Chandrika Bandaranayaka",
                    AnswerTwo = "Margaret Thatcher",
                    AnswerThree = "Sonia Gandhi",
                    AnswerExplanation = "Chandrika Bandaranayaka of Sri Lanka was the first female to be a president",
                    CorrectAnswerIndex = 1

                },
                 new Question
                {
                    Id = 7,
                    QuestionText = "Who betrayed Jesus?",
                    AnswerOne = "Paul",
                    AnswerTwo = "Judas",
                    AnswerThree = "Mohamed",
                    AnswerExplanation = "Judas betrayed Jesus for thirty silver coins",
                    CorrectAnswerIndex = 2

                }
            };



        }
        public async Task<PagedList<Question>> GetQuestions(UserParams userParams)
        {
            //var questions = _questions.AsQueryable();

            if (!string.IsNullOrEmpty(userParams.SearchTerm))
            {
                var filteredQuestions = _questions.Where(q => q.QuestionText.ToLower()
                                                  .Contains(userParams.SearchTerm.ToLower())).ToList();
                return await  PagedList<Question>.CreateAsync(filteredQuestions, userParams.PageNumber, userParams.PageSize);
            }

            return await  PagedList<Question>.CreateAsync(_questions, userParams.PageNumber, userParams.PageSize);
        } 

        public Question GetQuestion(int id)
        {
            return _questions.Find(x => x.Id == id);
        }

        public Question UpdateQuestion(Question updateQuestion)
        {
            var question = _questions.Find(q => q.Id == updateQuestion.Id);

            if (question != null)
            {
                question.QuestionCategory = updateQuestion.QuestionCategory;
                question.QuestionText = updateQuestion.QuestionText;
                question.AnswerOne = updateQuestion.AnswerOne;
                question.AnswerTwo = updateQuestion.AnswerTwo;
                question.AnswerThree = updateQuestion.AnswerThree;                
                question.AnswerExplanation = updateQuestion.AnswerExplanation;
                question.CorrectAnswerIndex = updateQuestion.CorrectAnswerIndex;
            }

            return question;
        }

        public Question CreateQuestion(Question newQuestion)
        {
            var id = _questions.Max(q => q.Id);
             newQuestion.Id = id+1;   
            _questions.Add(newQuestion);

            return newQuestion;
        }

        public Question DeleteQuestion(int id)
        {
            var questionToDelete = _questions.Find(x => x.Id==id);

            if (questionToDelete != null)
                _questions.Remove(questionToDelete);

            return questionToDelete;    
        }
    }
}
