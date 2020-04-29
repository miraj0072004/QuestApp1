namespace QuestionRevisedApi.Dtos
{
    public class QuestionCreateDto
    {
        
        public string QuestionText { get; set; }
        public string QuestionCategory {get;set;}
        public string AnswerOne { get; set; }
        public string AnswerTwo { get; set; }
        public string AnswerThree { get; set; }
        public int CorrectAnswerIndex { get; set; }
        public string AnswerExplanation { get; set; }
    }
}