using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using QuestionRevisedApi.Helpers;
using QuestionRevisedApi.Models;

namespace QuestionRevisedApi.Data
{
    public class SqlQuestionRepository : IQuestionsRepository
    {
        private readonly QuestionsRevisedContext _questionsRevisedContext;
        private readonly IMapper _mapper;

        public SqlQuestionRepository(QuestionsRevisedContext questionsRevisedContext, IMapper mapper)
        {
            _mapper = mapper;
            _questionsRevisedContext = questionsRevisedContext;

        }

        public async Task<Question> CreateQuestion(Question newQuestion)
        {
            await _questionsRevisedContext.AddAsync(newQuestion);
            await _questionsRevisedContext.SaveChangesAsync();

            return newQuestion;
        }

        public async Task<Question> DeleteQuestion(int id)
        {
            var QuestionToDelete = await _questionsRevisedContext.Questions.FindAsync(id);

            _questionsRevisedContext.Questions.Remove(QuestionToDelete);

            await _questionsRevisedContext.SaveChangesAsync();

            return QuestionToDelete;

        }

        public async Task<Question> GetQuestion(int id)
        {
            return await _questionsRevisedContext.Questions.FindAsync(id);
        }

        public async Task<List<int>> GetQuestionIds()
        {
            return _questionsRevisedContext.Questions.Select(q => q.Id).ToList();
        }

        public async Task<PagedList<Question>> GetQuestions(UserParams userParams)
        {
            if (!string.IsNullOrEmpty(userParams.SearchTerm))
            {
                var filteredQuestions = _questionsRevisedContext.Questions.Where(q => q.QuestionText.ToLower()
                                                  .Contains(userParams.SearchTerm.ToLower())).ToList();
                return await PagedList<Question>.CreateAsync(filteredQuestions, userParams.PageNumber, userParams.PageSize);
            }

            return await PagedList<Question>.CreateAsync(_questionsRevisedContext.Questions.ToList(), userParams.PageNumber, userParams.PageSize);
        }

        public async Task<Question> UpdateQuestion(Question updateQuestion)
        {
            // var question = _questionsRevisedContext.Questions.Attach(updateQuestion);
            var QuestionToUpdate = await _questionsRevisedContext.Questions.FindAsync(updateQuestion.Id);

            _mapper.Map(updateQuestion,QuestionToUpdate);
            
            // question.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _questionsRevisedContext.SaveChangesAsync();

            return updateQuestion;
        }




    }
}