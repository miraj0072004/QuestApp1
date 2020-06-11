using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionRevisedApi.Dtos
{
    public class UserForClientDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswerCount { get; set; }
        public int TotalGamesCount { get; set; }
    }
}
