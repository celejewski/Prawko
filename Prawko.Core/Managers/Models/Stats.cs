using System.Collections.Generic;
using System.Linq;

namespace Prawko.Core.Managers.Models
{
    public class Stats
    {
        internal Stats(IEnumerable<ProgressTracker> progressTrackers)
        {
            AttemptsTotal = progressTrackers.Sum(pt => pt.GetAttemptCount());
            QuestionsExplored = progressTrackers.Count(pt => pt.GetAttemptCount() > 0);
            QuestionsTotal = progressTrackers.Count();
        }

        public readonly int AttemptsTotal;

        public readonly int QuestionsTotal;
        public readonly int QuestionsExplored;
        public float QuestionsExploredPercentage => 100f * QuestionsExplored / QuestionsTotal;
    }
}
