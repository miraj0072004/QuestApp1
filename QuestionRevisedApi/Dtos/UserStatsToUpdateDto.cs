namespace QuestionRevisedApi.Dtos
{
    public class UserStatsToUpdateDto
    {        
        public int TotalQuestions { get; set; }
        public int CorrectAnswerCount { get; set; }
        public int TotalGamesCount { get; set; }
    }
}