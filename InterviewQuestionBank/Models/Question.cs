namespace InterviewQuestionBank.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string Answer { get; set; }
        public string Difficulty { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } // For display
    }

}
