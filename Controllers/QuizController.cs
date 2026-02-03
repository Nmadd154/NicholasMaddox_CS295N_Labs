using Microsoft.AspNetCore.Mvc;
using MvcEldenRingBossLore.Models;

namespace MvcEldenRingBossLore.Controllers;

public class QuizController : Controller
    {
        public Dictionary<int, String> Questions { get; set; }
        public Dictionary<int, String> Answers { get; set; }

        public QuizController()
        {
            Questions = new Dictionary<int, String>();
            Answers = new Dictionary<int, String>();
            
            // Temporary Questions
            Questions[1] = "What is the name of the final boss in Elden Ring?";
            Answers[1] = "Radagon of the Golden Order";
            Questions[2] = "Which boss is known for wielding a giant hammer?";
            Answers[2] = "Godrick the Grafted";
            Questions[3] = "Who is the boss that guards the entrance to the Stormhills?";
            Answers[3] = "Margit, the Fell Omen";
            Questions[4] = "Which boss is fought in the Cathedral of Manus Celest?";
            Answers[4] = "Rennala, Queen of the Full Moon";
            Questions[5] = "What is the name of the dragon boss found in the Mountaintops of the Giants?";
            Answers[5] = "Ancient Dragon Lansseax";
        }

        public IActionResult Index()
        {
            var model = LoadQuestions(new QuizQuestions());
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(string answer1, string answer2, string answer3, string answer4, string answer5)
        {
            var model = LoadQuestions(new QuizQuestions());
            model.UserAnswers[1] = answer1;
            model.UserAnswers[2] = answer2;
            model.UserAnswers[3] = answer3;
            model.UserAnswers[4] = answer4;
            model.UserAnswers[5] = answer5;

            var checkedModel = CheckAnswers(model);
            return View(checkedModel);
        }

        private QuizQuestions LoadQuestions(QuizQuestions model)
        {
            model.Questions = Questions;
            model.Answers = Answers;
            model.UserAnswers = new Dictionary<int, string>();
            model.Results = new Dictionary<int, bool>();
            
            foreach (var question in Questions)
            {
                int key = question.Key;
                model.UserAnswers[key] = "";
            }
            return model;
        }

        public QuizQuestions CheckAnswers(QuizQuestions model)
        {
            foreach (var question in Questions)
            {
                int key = question.Key;
                model.Results[key] = model.Answers[key] == model.UserAnswers[key];
            }
            return model;
        }
}