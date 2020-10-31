using Ninject;
using Prawko.Core.Managers;
using Prawko.Core.Managers.Enums;
using Prawko.Core.Managers.Models;
using Prawko.Core.Managers.Providers;
using Prawko.Core.Managers.QuestionSelectors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prawko.Core
{
    public class DomainFacade
    {
        private readonly QuestionManager _questionManager;
        private readonly ProgressTrackerManager _progressTrackerManager;
        private readonly IProgressStorage _progressStorage;
        private readonly Random _random = new Random();
        private readonly IKernel _kernel;
        public DomainFacade(string questionFile, MediaInfoBaseProvider mediaInfoBaseProvider, IProgressStorage progressStorage)
        {
            _progressStorage = progressStorage;

            _kernel = new StandardKernel();
            _kernel.Bind<MediaInfoBaseProvider>()
                .ToConstant(mediaInfoBaseProvider);

            _progressTrackerManager = _progressStorage.HasSave
                ? ProgressTrackerManager.Load(_progressStorage)
                : _kernel.Get<ProgressTrackerManager>();

            _kernel.Bind<ProgressTrackerManager>()
                .ToConstant(_progressTrackerManager);

            _questionManager = _kernel.Get<QuestionManager>();
            _questionManager.LoadQuestionsFromFile(questionFile);
        }

        public IEnumerable<Question> SelectExamQuestions(Language language, DrivingLicenseCategory drivingLicenseCategory)
        {
            return SelectQuestions<QuestionSelectorExam>(language, drivingLicenseCategory);
        }

        public IEnumerable<Question> SelectReviewQuestions(Language language, DrivingLicenseCategory drivingLicenseCategory)
        {
            return SelectQuestions<QuestionSelectorReview>(language, drivingLicenseCategory);
        }

        private IEnumerable<Question> SelectQuestions<T>(Language language, DrivingLicenseCategory drivingLicenseCategory)
            where T : IQuestionSelector
        {
            var filteredQuestions = _questionManager.GetQuestions(language, drivingLicenseCategory);
            var shuffledQuestions = filteredQuestions.OrderBy(_ => _random.Next());
            return _kernel
                .Get<T>()
                .SelectFrom(shuffledQuestions);
        }

        public Question GetQuestion(Language language, int id)
        {
            return _questionManager.GetQuestion(language, id);
        }

        public void AddScore(int questionId, float score)
        {
            _progressTrackerManager.AddScore(questionId, score);
            _progressTrackerManager.Save(_progressStorage);
        }
    }
}
