using Npoi.Mapper.Attributes;

namespace Prawko.Core.Managers.Excel
{
    internal sealed class ExcelRow
    {
        [Column("Numer pytania")]
        public int QuestionId { get; set; }
        [Column("Pytanie")]
        public string PLQuestion { get; set; }
        [Column("Odpowiedź A")]
        public string PLAnswerA { get; set; }
        [Column("Odpowiedź B")]
        public string PLAnswerB { get; set; }
        [Column("Odpowiedź C")]
        public string PLAnswerC { get; set; }
        [Column("Pytanie ENG")]
        public string ENGQuestion { get; set; }
        [Column("Odpowiedź ENG A")]
        public string ENGAnswerA { get; set; }
        [Column("Odpowiedź ENG B")]
        public string ENGAnswerB { get; set; }
        [Column("Odpowiedź ENG C")]
        public string ENGAnswerC { get; set; }
        [Column("Pytanie DE")]
        public string DEQuestion { get; set; }
        [Column("Odpowiedź DE A")]
        public string DEAnswerA { get; set; }
        [Column("Odpowiedź DE B")]
        public string DEAnswerB { get; set; }
        [Column("Odpowiedź DE C")]
        public string DEAnswerC { get; set; }
        [Column("Poprawna odp")]
        public string CorrectAnswer { get; set; }
        [Column("Media")]
        public string MediaFilename { get; set; }
        [Column("Zakres struktury")]
        public string QuestionCategory { get; set; }
        [Column("Liczba punktów")]
        public int Points { get; set; }
        [Column("Kategorie")]
        public string DrivingLicenseCategories { get; set; }
        [Column("Nazwa bloku")]
        public string BlockName { get; set; }
        [Column("Nazwa media tłumaczenie migowe (PJM) treść pyt")]
        public string SignLanguageQuestion { get; set; }
        [Column("Nazwa media tłumaczenie migowe (PJM) treść odp A")]
        public string SignLanguageAnswerA { get; set; }
        [Column("Nazwa media tłumaczenie migowe (PJM) treść odp B")]
        public string SignLanguageAnswerB { get; set; }
        [Column("Nazwa media tłumaczenie migowe (PJM) treść odp c")]
        public string SignLanguageAnswerC { get; set; }
    }
}
