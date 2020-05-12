using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuestionRevisedApi.Models;

namespace QuestionRevisedApi.Data
{
    public interface IQuestionsRepository
    {
        public IEnumerable<Question> GetQuestions();
        public Question GetQuestion(int id);

        public Question UpdateQuestion(Question updateQuestion);

        public Question CreateQuestion(Question newQuestion);

        public Question DeleteQuestion(int id);
        
    }
}
