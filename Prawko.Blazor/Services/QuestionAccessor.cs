using Prawko.Core;
using Prawko.Core.Managers.Enums;
using Prawko.Core.Managers.Models;
using Prawko.Core.Managers.Providers;
using System.Collections.Generic;
using System.Linq;

namespace Prawko.Blazor.Services
{
    public class QuestionAccessor
    {

        private readonly DomainFacade _domainFacade;
        public QuestionAccessor(IProgressStorage progressStorage)
        {
            _domainFacade = new DomainFacade(
                @"C:\Users\xyz\Downloads\PRAWOJAZDY\baza.xlsx",
                new MediaInfoLocalFileProvider("media"),
                progressStorage
            );
        }

        public Question GetQuestion(Language language, int id)
        {
            return _domainFacade.GetQuestion(language, id);
        }

        public List<Question> SelectReviewQuestions(Language language, DrivingLicenseCategory drivingLicenseCategory)
        {
            return _domainFacade.SelectReviewQuestions(language, drivingLicenseCategory).ToList();
        }

        public List<Question> SelectExamQuestions(Language language, DrivingLicenseCategory drivingLicenseCategory)
        {
            return _domainFacade.SelectExamQuestions(language, drivingLicenseCategory).ToList();
        }

        public void AddScore(int questionId, float score)
        {
            _domainFacade.AddScore(questionId, score);
        }
    }
}
