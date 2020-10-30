using Prawko.Core;
using Prawko.Core.Managers;
using Prawko.Core.Managers.Enums;
using System;
using System.Linq;

namespace Prawko.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string questionFile = @"C:\Users\xyz\Downloads\PRAWOJAZDY\baza.xlsx";
            var mediaInfoLocalFileProvider = new MediaInfoLocalFileProvider(@"C:\Users\xyz\Downloads\PRAWOJAZDY\Converted720pYT");
            var domainFacade = new DomainFacade(questionFile, mediaInfoLocalFileProvider, new LocalProgressStorage());
            domainFacade.AddScore(-1, 1);
            var exam = domainFacade.SelectReviewQuestions(Language.PL, DrivingLicenseCategory.B).ToList();
            System.Console.WriteLine(exam.Count());
        }
    }
}
