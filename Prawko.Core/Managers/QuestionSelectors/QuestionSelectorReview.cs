using Prawko.Core.Managers.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prawko.Core.Managers.QuestionSelectors
{
    public class QuestionSelectorReview : IQuestionSelector
    {
        private readonly ProgressTrackerManager _progressTrackerManager;

        public QuestionSelectorReview(ProgressTrackerManager progressTrackerManager)
        {
            _progressTrackerManager = progressTrackerManager;
        }

        public IEnumerable<Question> SelectFrom(IEnumerable<Question> questions)
        {
            return questions
                .OrderBy(q => _progressTrackerManager.GetScoreOfLastAttempts(q.Id, 5))
                .Take(20);
        }
    }
}
