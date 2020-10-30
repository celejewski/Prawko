using System;
using System.Collections.Generic;
using System.Linq;

namespace Prawko.Core.Managers.Models
{
    public class ProgressTracker
    {
        public int Id { get; set; }

        public List<float> Scores { get; set; } = new List<float>();

        public float GetScoreTotal() => Scores.Sum();
        public float GetScoreOfLastAttempts(int attempsLimit) => Scores.TakeLast(attempsLimit).Sum();

        public DateTime LastScored { get; set; } = DateTime.MinValue;

        public float GetAttemptCount() => Scores.Count;
    }
}
