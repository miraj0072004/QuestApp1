using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuestionRevisedApi.Helpers;
using QuestionRevisedApi.Models;

namespace QuestionRevisedApi.Data
{
    public interface IQuestionsRepository
    {
        public Task<PagedList<Question>> GetQuestions(UserParams userParams);
        public Question GetQuestion(int id);

        public Question UpdateQuestion(Question updateQuestion);

        public Question CreateQuestion(Question newQuestion);

        public Question DeleteQuestion(int id);
        
    }
}
