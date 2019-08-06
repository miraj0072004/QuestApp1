namespace QuestApp1.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionAnswerId { get; set; }
        public string AnswerText { get; set; }
        public bool Correctness { get; set; }
    }
}