using Prawko.Core.Managers.Models;
using Prawko.Core.Managers.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Prawko.Core.Managers
{
    public class ProgressTrackerManager
    {
        private readonly Dictionary<int, ProgressTracker> _progressTrackers;

        public static ProgressTrackerManager Load(IProgressStorage progressStorage)
        {
            var serialized = progressStorage.Load();
            var progressTrackers = JsonSerializer.Deserialize<List<ProgressTracker>>(serialized);
            var keyValuePairs = progressTrackers.Select(pt => new KeyValuePair<int, ProgressTracker>(pt.Id, pt));
            var dictionary = new Dictionary<int, ProgressTracker>(keyValuePairs);
            return new ProgressTrackerManager(dictionary);
        }

        public ProgressTrackerManager()
            : this(new Dictionary<int, ProgressTracker>())
        {
        }
        private ProgressTrackerManager(Dictionary<int, ProgressTracker> progressTrackers)
        {
            _progressTrackers = progressTrackers;
        }

        private ProgressTracker GetProgressTracker(int id)
        {
            if( !_progressTrackers.ContainsKey(id) )
            {
                _progressTrackers.Add(id, new ProgressTracker { Id = id });
            }

            return _progressTrackers[id];
        }

        public float GetScoreTotal(int id)
        {
            return GetProgressTracker(id).GetScoreTotal();
        }

        public float GetScoreOfLastAttempts(int id, int attemptsLimit)
        {
            return GetProgressTracker(id).GetScoreOfLastAttempts(attemptsLimit);
        }

        public float GetAttemptCount(int id)
        {
            return GetProgressTracker(id).GetAttemptCount();
        }

        public void AddScore(int id, float score)
        {
            var progressTracker = GetProgressTracker(id);
            progressTracker.Scores.Add(score);
            progressTracker.LastScored = DateTime.Now;
        }

        public void Save(IProgressStorage progressStorage)
        {
            var serialized = JsonSerializer.Serialize(_progressTrackers.Values.ToArray());
            progressStorage.Save(serialized);
        }
    }
}
