using System;
using System.Collections.Generic;
using System.Text;

namespace QuestApp1.Models
{
    public class UserFromApi
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
