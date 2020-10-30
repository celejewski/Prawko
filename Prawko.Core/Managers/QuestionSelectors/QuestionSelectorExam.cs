using Prawko.Core.Managers.Enums;
using Prawko.Core.Managers.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prawko.Core.Managers.QuestionSelectors
{
    public class QuestionSelectorExam : IQuestionSelector
    {
        private readonly Random _random;
        public QuestionSelectorExam()
        {
            _random = new Random();
        }
        public IEnumerable<Question> SelectFrom(IEnumerable<Question> questions)
        {
            var basicQuestions = questions.Where(q => q.QuestionCategory == QuestionCategory.Basic);
            var specialistQuestions = questions.Where(q => q.QuestionCategory == QuestionCategory.Specialist);

            return Draw(basicQuestions, 10, 6, 4)
                .Concat(Draw(specialistQuestions, 6, 4, 2));
        }

        private IEnumerable<Question> Draw(IEnumerable<Question> questions, int limit3Points, int limit2Points, int limit1Point)
        {
            var questionsFor3Points = questions.Where(q => q.Points == 3).Take(limit3Points);
            var questionsFor2Points = questions.Where(q => q.Points == 2).Take(limit2Points);
            var questionsFor1Point = questions.Where(q => q.Points == 1).Take(limit1Point);

            return questionsFor3Points
                .Concat(questionsFor2Points)
                .Concat(questionsFor1Point)
                .OrderBy(_ => _random.Next());
        }
    }
}
