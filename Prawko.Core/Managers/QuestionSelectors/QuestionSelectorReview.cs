using Prawko.Core.Managers.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prawko.Core.Managers.QuestionSelectors
{
    public class QuestionSelectorReview : IQuestionSelector
    {
        private readonly ProgressTrackerManager _progressTrackerManager;
        private static readonly Random _random = new Random();

        public QuestionSelectorReview(ProgressTrackerManager progressTrackerManager)
        {
            _progressTrackerManager = progressTrackerManager;
        }

        public IEnumerable<Question> SelectFrom(IEnumerable<Question> questions)
        {
            return questions
                .OrderBy(q => _progressTrackerManager.GetScoreOfLastAttempts(q.Id, 5))
                .ThenBy(q => GetRandomIntBasedOnPoints(q.Points))
                .Take(20);
        }

        private static int GetRandomIntBasedOnPoints(int points)
        {
            return Enumerable
                .Range(0, points)
                .Select(_ => _random.Next())
                .Min();
        }
    }
}
