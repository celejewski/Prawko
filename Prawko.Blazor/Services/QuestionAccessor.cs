using Microsoft.Extensions.Options;
using Prawko.Blazor.Configs;
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
        public QuestionAccessor(
            IOptions<DirectoryOptions> options,
            MediaInfoLocalFileProvider mediaInfoLocalFileProvider,
            IProgressStorage progressStorage)
        {
            _domainFacade = new DomainFacade(
                options.Value.QuestionFullPath,
                mediaInfoLocalFileProvider,
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
