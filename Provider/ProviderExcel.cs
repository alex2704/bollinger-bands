using ClassLibrary;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Provider
{
    public class ProviderExcel : FileDataProvider
    {
        public ProviderExcel(OpenFileDialog openFileDialog) : base(openFileDialog)
        {
        }
        protected override void ReadDataFromFile(OpenFileDialog openFileDialog)
        {
            ////Создаем приложение
            //Microsoft.Office.Interop.Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
            ////Открываем книгу
            //Microsoft.Office.Interop.Excel.Workbook ObjWorkBook = ObjExcel.Workbooks.Open(openFileDialog.FileName);
            ////Выбираем таблицу лист
            //Microsoft.Office.Interop.Excel.Worksheet WorksheetExcel = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];
            //var lastCell = WorksheetExcel.Cells.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeLastCell);
            //int k = (int)lastCell.Row;
            //for (int i = 2; i < (int)lastCell.Row; i++)
            //{
            //    var dateValue = WorksheetExcel.Cells[i, 3].Value;
            //    var timeValue = WorksheetExcel.Cells[i, 4].Value;
            //    var openValue = Convert.ToDecimal((WorksheetExcel.Cells[i, 5]).Value) / 1e5m;
            //    var highValue = Convert.ToDecimal((WorksheetExcel.Cells[i, 6]).Value) / 1e5m;
            //    var lowValue = Convert.ToDecimal((WorksheetExcel.Cells[i, 7]).Value) / 1e5m;
            //    var closeValue = Convert.ToDecimal((WorksheetExcel.Cells[i, 8]).Value) / 1e5m;
            //    var date = DateTime.ParseExact($"{dateValue} {timeValue}", "yyyyMMdd HHmmss", CultureInfo.InvariantCulture);
            //    Candle candle = new Candle(highValue, lowValue, openValue, closeValue, date);
            //    Candles.Enqueue(candle);
            //}
            using (var package = new ExcelPackage(new FileInfo(openFileDialog.FileName)))
            {
                var worksheet = package.Workbook.Worksheets[1];
                for(int i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
                {
                    var dateValue = worksheet.Cells[i, 3].Value;
                    var timeValue = worksheet.Cells[i, 4].Value;
                    var openValue = Convert.ToDecimal((worksheet.Cells[i, 5]).Value) / 1e5m;
                    var highValue = Convert.ToDecimal((worksheet.Cells[i, 6]).Value) / 1e5m;
                    var lowValue = Convert.ToDecimal((worksheet.Cells[i, 7]).Value) / 1e5m;
                    var closeValue = Convert.ToDecimal((worksheet.Cells[i, 8]).Value) / 1e5m;
                    var date = DateTime.ParseExact($"{dateValue} {timeValue}", "yyyyMMdd HHmmss", CultureInfo.InvariantCulture);
                    Candle candle = new Candle(highValue, lowValue, openValue, closeValue, date);
                    Candles.Enqueue(candle);
                }
            }
        }
    }
}
