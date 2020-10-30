using Prawko.Core.Managers.Enums;
using Prawko.Core.Managers.Excel;
using Prawko.Core.Managers.Factories;
using Prawko.Core.Managers.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prawko.Core.Managers
{
    internal sealed class QuestionManager
    {
        private readonly QuestionFactory _questionFactory;

        public QuestionManager(QuestionFactory questionFactory)
        {
            _questionFactory = questionFactory;
        }

        public void LoadQuestionsFromFile(string path)
        {
            var excelRows = ExcelReader.ReadFile(path);
            foreach( var excelRow in excelRows )
            {
                _questions.Add(_questionFactory.MakeQuestionPL(excelRow));
                _questions.Add(_questionFactory.MakeQuestionDE(excelRow));
                _questions.Add(_questionFactory.MakeQuestionENG(excelRow));
            }
        }

        public IEnumerable<Question> GetQuestions(Language language, DrivingLicenseCategory drivingLicenseCategory)
        {
            return _questions
                .Where(q => q.Language == language
                    && q.DrivingLicenseCategories.Contains(drivingLicenseCategory));
        }

        private readonly List<Question> _questions = new List<Question>();
    }
}
