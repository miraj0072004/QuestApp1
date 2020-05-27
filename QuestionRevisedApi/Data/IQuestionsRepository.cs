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
        public Task<Question> GetQuestion(int id);

        public Task<Question> UpdateQuestion(Question updateQuestion);

        public Task<Question> CreateQuestion(Question newQuestion);

        public Task<Question> DeleteQuestion(int id);
        
    }
}
