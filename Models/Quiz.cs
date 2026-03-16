// Created by Nicholas Maddox
namespace MvcEldenRingBossLore.Models
{
    public class QuizQuestions
    {
        public Dictionary<int, string> Questions { get; set; } = new();
        public Dictionary<int, string> Answers { get; set; } = new();
        public Dictionary<int, string> UserAnswers { get; set; } = new();
        public Dictionary<int, bool> Results { get; set; } = new();
    }
}