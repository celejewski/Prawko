using Prawko.Core.Managers.Enums;
using System.Collections.Generic;

namespace Prawko.Core.Managers.Models
{
    public class Question
    {
        public Language Language { get; set; }
        public int Id { get; set; }
        public QuestionCategory QuestionCategory { get; set; }
        public string Text { get; set; }
        public int Points { get; set; }
        public MediaInfo MediaInfo { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
        public IEnumerable<DrivingLicenseCategory> DrivingLicenseCategories { get; set; }
    }
}
