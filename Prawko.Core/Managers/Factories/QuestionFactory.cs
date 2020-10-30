using Prawko.Core.Managers.Enums;
using Prawko.Core.Managers.Excel;
using Prawko.Core.Managers.Models;
using Prawko.Core.Managers.Providers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prawko.Core.Managers.Factories
{
    internal class QuestionFactory
    {
        private readonly MediaInfoBaseProvider _mediaInfoProvider;
        public QuestionFactory(MediaInfoBaseProvider mediaInfoProvider)
        {
            _mediaInfoProvider = mediaInfoProvider;
        }

        public Question MakeQuestionPL(ExcelRow excelRow)
        {
            if( IsTrueOrFalseQuestion(excelRow) )
            {
                return MakeQuestion(excelRow, Language.PL, excelRow.PLQuestion, "Tak", "Nie");
            }
            return MakeQuestion(excelRow, Language.PL, excelRow.PLQuestion, excelRow.PLAnswerA, excelRow.PLAnswerB, excelRow.PLAnswerC);
        }

        public Question MakeQuestionDE(ExcelRow excelRow)
        {
            if( IsTrueOrFalseQuestion(excelRow) )
            {
                return MakeQuestion(excelRow, Language.DE, excelRow.DEQuestion, "Ja", "Nein");
            }
            return MakeQuestion(excelRow, Language.DE, excelRow.DEQuestion, excelRow.DEAnswerA, excelRow.DEAnswerB, excelRow.DEAnswerC);
        }

        public Question MakeQuestionENG(ExcelRow excelRow)
        {
            if( IsTrueOrFalseQuestion(excelRow) )
            {
                return MakeQuestion(excelRow, Language.ENG, excelRow.ENGQuestion, "Yes", "No");
            }
            return MakeQuestion(excelRow, Language.DE, excelRow.ENGQuestion, excelRow.ENGAnswerA, excelRow.ENGAnswerB, excelRow.ENGAnswerC);
        }

        private static bool IsTrueOrFalseQuestion(ExcelRow excelRow)
        {
            return string.IsNullOrWhiteSpace(excelRow.PLAnswerA);
        }

        private Question MakeQuestion(ExcelRow excelRow, Language language, string text, params string[] answerTexts)
        {
            return new Question
            {
                Id = excelRow.QuestionId,
                Language = language,
                Points = excelRow.Points,
                MediaInfo = _mediaInfoProvider.GetMediaInfo(excelRow.MediaFilename),
                Text = text,
                Answers = ConvertToAnswers(answerTexts, excelRow.CorrectAnswer),
                DrivingLicenseCategories = ConvertToDrivingLicenseCategories(excelRow.DrivingLicenseCategories),
                QuestionCategory = ConvertToQuestionCategory(excelRow.QuestionCategory),
            };
        }

        private static QuestionCategory ConvertToQuestionCategory(string questionCategory)
        {
            return questionCategory switch
            {
                "PODSTAWOWY" => QuestionCategory.Basic,
                "SPECJALISTYCZNY" => QuestionCategory.Specialist,
                _ => throw new ArgumentException($"Can not convert {questionCategory} to enum {nameof(QuestionCategory)} value."),
            };
        }

        private static List<Answer> ConvertToAnswers(IList<string> answersText, string correctAnswer)
        {
            var indexOfCorrectAnswer = correctAnswer switch
            {
                "A" => 0,
                "B" => 1,
                "C" => 2,
                "T" => 0,
                "N" => 1,
                _ => throw new ArgumentException($"Provided correct answer = {correctAnswer} can not be converted to answer index."),
            };

            if( indexOfCorrectAnswer > answersText.Count )
            {
                throw new ArgumentException($"Correct answer index = {correctAnswer} is out of range please provide more answers = {answersText}");
            }

            var answers = answersText
                .Select(at => new Answer { Text = at })
                .ToList();
            answers[indexOfCorrectAnswer].IsCorrect = true;
            return answers;
        }

        private static List<DrivingLicenseCategory> ConvertToDrivingLicenseCategories(string drivingLicenseCategoriesText)
        {
            var categoriesSplitted = drivingLicenseCategoriesText.Split(',');
            return categoriesSplitted
                .Select(c => Enum.Parse<DrivingLicenseCategory>(c))
                .ToList();
        }
    }
}
