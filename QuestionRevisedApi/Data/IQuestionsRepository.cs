using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuestionRevisedApi.Models;

namespace QuestionRevisedApi.Data
{
    public interface IQuestionsRepository
    {
        public List<Question> GetQuestions();
        public Question GetQuestion(int id);
    }
}
