using System.Threading.Tasks;
using QuestionRevisedApi.Helpers;
using QuestionRevisedApi.Models;

namespace QuestionRevisedApi.Data
{
    public interface IUsersRepository
    {
        Task<PagedList<User>> GetUsers(UserParams userParams);
        Task<User> GetUser(int userId);
        Task<User> UpdateUser(User updatedUser);
        Task<User> DeleteUser(int userId);

        Task<User> UpdateStats(User userWithStatsUpdated);
    }
}