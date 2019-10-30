using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestApp1.Models
{
    public class UserPerformance
    {
       
        public string UserId { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswerCount { get; set; }
        public int TotalGamesCount { get; set; }
        
    }
}