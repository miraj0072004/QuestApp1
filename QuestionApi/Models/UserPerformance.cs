using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuestionApi.Models
{
    public class UserPerformance
    {
        [Key]
        public string UserId { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswerCount { get; set; }
        public int TotalGamesCount { get; set; }
        
    }
}