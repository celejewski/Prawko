using Npoi.Mapper;
using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Prawko.Core.Managers.Excel
{
    internal static class ExcelReader
    {
        public static List<ExcelRow> ReadFile(string path)
        {
            IWorkbook workbook;
            using( var fileStream = File.OpenRead(path) )
            {
                workbook = WorkbookFactory.Create(fileStream);
            }

            var mapper = new Mapper(workbook);
            var excelRows = mapper.Take<ExcelRow>(sheetIndex: 0)
                .Select(rowInfo => rowInfo.Value)
                .ToList();

            foreach( var excelRow in excelRows )
            {
                excelRow.ENGQuestion = RemoveDuplicates(excelRow.ENGQuestion);
                excelRow.DEQuestion = RemoveDuplicates(excelRow.DEQuestion);
            }
            return excelRows;
        }

        private static string RemoveDuplicates(string value)
        {
            var indexOfNewLine = value.IndexOf('\n');
            return indexOfNewLine == -1
                ? value
                : value.Remove(indexOfNewLine);
        }
    }
}
