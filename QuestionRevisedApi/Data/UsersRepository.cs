using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using QuestionRevisedApi.Helpers;
using QuestionRevisedApi.Models;

namespace QuestionRevisedApi.Data
{
    public class UsersRepository : IUsersRepository
    {
        private readonly QuestionsRevisedContext _questionsRevisedContext;
        private readonly IMapper _mapper;

        public UsersRepository(QuestionsRevisedContext questionsRevisedContext, IMapper mapper)
        {
            _mapper = mapper;
            _questionsRevisedContext = questionsRevisedContext;

        }
        public async Task<User> DeleteUser(int userId)
        {
            var userToDelete = await _questionsRevisedContext.Users.FindAsync(userId);
            _questionsRevisedContext.Users.Remove(userToDelete);
            await _questionsRevisedContext.SaveChangesAsync();
            return userToDelete;
        }

        public async Task<User> GetUser(int userId)
        {
            return await _questionsRevisedContext.Users.FindAsync(userId);
        }

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            if (!string.IsNullOrEmpty(userParams.SearchTerm))
            {
                var filteredUsers = _questionsRevisedContext.Users.Where(u => u.Username.ToLower()
                                                  .Contains(userParams.SearchTerm.ToLower())).ToList();
                return await PagedList<User>.CreateAsync(filteredUsers, userParams.PageNumber, userParams.PageSize);
            }

            return await PagedList<User>.CreateAsync(_questionsRevisedContext.Users.ToList(), userParams.PageNumber, userParams.PageSize);
        }

        public async Task<User> UpdateStats(User userWithStatsUpdated)
        {
            var user = await _questionsRevisedContext.Users.FindAsync(userWithStatsUpdated.Id);

            user.TotalQuestions = userWithStatsUpdated.TotalQuestions;
            user.CorrectAnswerCount = userWithStatsUpdated.CorrectAnswerCount;
            user.TotalGamesCount += 1;

            await _questionsRevisedContext.SaveChangesAsync();

            return user;
        }

        public async Task<User> UpdateUser(User updatedUser)
        {
            var userToUpdate = await _questionsRevisedContext.Users.FindAsync(updatedUser.Id);

            _mapper.Map(updatedUser,userToUpdate);
            
            // question.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _questionsRevisedContext.SaveChangesAsync();

            return userToUpdate;
        }
    }
}